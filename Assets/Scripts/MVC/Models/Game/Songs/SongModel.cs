using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongModel : ISongModel
{
    const float SKIP_TO_SECONDS_BEFORE = 2;
    
    public event Action<Note> OnNoteSpawned;
    public event Action<Note, double> OnNoteHit;
    public event Action<Note, double> OnLongNoteHit;
    public event Action<Note, double> OnLongNoteReleased;
    public event Action<Note> OnNoteMissed;
    public event Action OnAudioStartTimeReached;
    public event Action<float> OnSongStartSkipped;
    public event Action OnSongStarted;
    public event Action OnSongFinished;
    public event Action<bool> OnSkippableChanged;
    public event Action OnSongLoaded;

    public SongLoader SongLoader { get; }
    public ISongSettings CurrentSongSettings => SongLoader.GetSelectedSongSettings();
    public float ApproachRate => SettingsProvider.ApproachRate;
    public bool AllNotesRead { get; private set; }

    readonly IGameInputManager inputManager;

    bool skippable;
    bool Skippable
    {
        get => skippable;
        set
        {
            if (skippable == value)
                return;
            OnSkippableChanged?.Invoke(value);
            skippable = value;
        }
    }

    double dspStart;
    double dspSongStart;
    double pauseOffset;
    float skippedTime;

    IScoreModel scoreModel;

    public SongModel (IGameInputManager inputManager, SongLoader songLoader)
    {
        this.inputManager = inputManager;
        SongLoader = songLoader;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    public void UpdateDependencies (IScoreModel scoreModel)
    {
        this.scoreModel = scoreModel;
    }

    public void LoadSong (string songId, string songDifficultyName)
    {
        SongLoader.SelectedSongId = songId;
        SongLoader.SelectedSongDifficulty = songDifficultyName;
        OnSongLoaded?.Invoke();
    }

    public void Play ()
    {
        Skippable = false;
        dspStart = AudioSettings.dspTime;
        dspSongStart = AudioSettings.dspTime + GetStartingElapsed();
        
        CoroutineRunner.Instance.StartRoutine(nameof(AudioStartRoutine), AudioStartRoutine());
        CoroutineRunner.Instance.StartRoutine(nameof(NoteSpawnRoutine), NoteSpawnRoutine());
        CoroutineRunner.Instance.StartRoutine(nameof(NotesHitRoutine), NotesHitRoutine());
        CoroutineRunner.Instance.StartRoutine(nameof(PauseOffsetRoutine), PauseOffsetRoutine());
        
        OnSongStarted?.Invoke();
    }

    double GetStartingElapsed () => CurrentSongSettings.StartingTime < ApproachRate
        ? ApproachRate + CurrentSongSettings.StartingTime
        : CurrentSongSettings.StartingTime;
    
    double GetElapsedTime () => AudioSettings.dspTime - dspSongStart - pauseOffset + skippedTime;

    IEnumerator AudioStartRoutine ()
    {
        if (ApproachRate > CurrentSongSettings.StartingTime)
            yield return new WaitForSeconds(ApproachRate);
        Skippable = true;
        OnAudioStartTimeReached?.Invoke();
    }

    IEnumerator NoteSpawnRoutine ()
    {
        IReadOnlyList<Note> notes = CurrentSongSettings.Notes;
        int noteIndex = 0;
        while (true)
        {
            yield return null;
            
            double elapsed = GetElapsedTime();
            double noteSpawnTime = notes[noteIndex].Time - ApproachRate;
            
            if (elapsed > noteSpawnTime)
            {
                Skippable = false;
                OnNoteSpawned?.Invoke(notes[noteIndex]);
                if (++noteIndex >= notes.Count)
                    break;
            }
        }
    }

    IEnumerator NotesHitRoutine ()
    {
        IReadOnlyList<Note> notes = CurrentSongSettings.Notes;
        int noteIndex = 0;
        bool hittingCurrentLongNote = false;
        
        while (true)
        {
            if (noteIndex >= notes.Count)
                break;
            
            yield return null;

            Note currentNote = notes[noteIndex];

            double elapsed = GetElapsedTime();
            double timeToNote = currentNote.Time - elapsed;
            double timeToNoteEnd = currentNote.EndTime - elapsed;

            if (!currentNote.IsLong)
            {
                if (timeToNote < scoreModel.MaximumHitWindow && inputManager.GetPositionPressed(currentNote.Position))
                {
                    OnNoteHit?.Invoke(currentNote, timeToNote);
                    noteIndex++;
                    continue;
                }
            }
            else
            {
                if (hittingCurrentLongNote)
                {
                    if (timeToNoteEnd < 0 || !inputManager.GetPositionHeld(currentNote.Position))
                    {
                        hittingCurrentLongNote = false;
                        OnLongNoteReleased?.Invoke(currentNote, timeToNoteEnd);
                        noteIndex++;
                    }
                    continue;
                }
                
                if (timeToNote < scoreModel.MaximumHitWindow && inputManager.GetPositionPressed(currentNote.Position))
                {
                    OnLongNoteHit?.Invoke(currentNote, timeToNote);
                    hittingCurrentLongNote = true;
                    continue;
                }
            }
            
            if (timeToNote < -scoreModel.MaximumHitWindow)
            {
                OnNoteMissed?.Invoke(currentNote);
                noteIndex++;
            }
        }

        AllNotesRead = true;
        yield return new WaitForSeconds(scoreModel.MaximumHitWindow * 3);
        OnSongFinished?.Invoke();
    }

    IEnumerator PauseOffsetRoutine ()
    {
        while (true)
        {
            double lastDspTime = AudioSettings.dspTime;
            yield return null;
            if (GameManager.IsPaused)
                pauseOffset += AudioSettings.dspTime - lastDspTime;
        }
    }

    void SkipSongStart ()
    {
        if (skippedTime != 0 || !Skippable)
            return;
        Skippable = false;
        float threshold = Mathf.Max(ApproachRate, SKIP_TO_SECONDS_BEFORE);
        skippedTime = CurrentSongSettings.StartingTime + CurrentSongSettings.Notes[0].Time - threshold -
                      (float) (AudioSettings.dspTime - dspStart);
        OnSongStartSkipped?.Invoke(skippedTime);
    }

    void AddListeners ()
    {
        inputManager.OnSpacePressed += HandleSpacePressed;
    }

    void RemoveListeners ()
    {
        inputManager.OnSpacePressed -= HandleSpacePressed;
    }

    void HandleSpacePressed () => SkipSongStart();

    public void Dispose ()
    {
        RemoveListeners();
        CoroutineRunner.Instance.StopRoutine(nameof(AudioStartRoutine));
        CoroutineRunner.Instance.StopRoutine(nameof(NoteSpawnRoutine));
        CoroutineRunner.Instance.StopRoutine(nameof(NotesHitRoutine));
        CoroutineRunner.Instance.StopRoutine(nameof(PauseOffsetRoutine));
    }
}