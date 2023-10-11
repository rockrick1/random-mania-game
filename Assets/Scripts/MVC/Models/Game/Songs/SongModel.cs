using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongModel : ISongModel
{
    const float SKIP_TO_SECONDS_BEFORE = 2;
    
    public event Action<Note> OnNoteSpawned;
    public event Action<Note, HitScore> OnNoteHit;
    public event Action<Note, HitScore> OnLongNoteHit;
    public event Action<Note, HitScore> OnLongNoteReleased;
    public event Action<Note> OnNoteMissed;
    public event Action OnAudioStartTimeReached;
    public event Action<float> OnSongStartSkipped;
    public event Action OnSongStarted;
    public event Action OnSongFinished;
    public event Action<bool> OnSkippableChanged;

    public SongLoader SongLoader { get; }
    public ISongSettings CurrentSongSettings => SongLoader.GetSelectedSongSettings();
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

    float perfectHitWindow;
    float greatHitWindow;
    float okayHitWindow;

    double dspStart;
    double dspSongStart;
    double pauseOffset;
    float skippedTime;

    public SongModel (IGameInputManager inputManager, SongLoader songLoader)
    {
        this.inputManager = inputManager;
        SongLoader = songLoader;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    public void LoadSong (string songId, string songDifficultyName)
    {
        SongLoader.SelectedSongId = songId;
        SongLoader.SelectedSongDifficulty = songDifficultyName;
        perfectHitWindow = (80 - 6 * CurrentSongSettings.Difficulty) / 1000f;
        greatHitWindow = (140 - 8 * CurrentSongSettings.Difficulty) / 1000f;
        okayHitWindow = (200 - 10 * CurrentSongSettings.Difficulty) / 1000f;
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

    double GetStartingElapsed () => CurrentSongSettings.StartingTime < CurrentSongSettings.ApproachRate
        ? CurrentSongSettings.ApproachRate + CurrentSongSettings.StartingTime
        : CurrentSongSettings.StartingTime;
    
    double GetElapsedTime () => AudioSettings.dspTime - dspSongStart - pauseOffset + skippedTime;

    IEnumerator AudioStartRoutine ()
    {
        if (CurrentSongSettings.ApproachRate > CurrentSongSettings.StartingTime)
            yield return new WaitForSeconds(CurrentSongSettings.ApproachRate);
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
            double noteSpawnTime = notes[noteIndex].Time - CurrentSongSettings.ApproachRate;
            
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
                if (timeToNote < okayHitWindow && inputManager.GetPositionPressed(currentNote.Position))
                {
                    OnNoteHit?.Invoke(currentNote, GetHitScore(timeToNote));
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
                        OnLongNoteReleased?.Invoke(currentNote, GetHitScore(timeToNoteEnd));
                        noteIndex++;
                    }
                    continue;
                }
                
                if (timeToNote < okayHitWindow && inputManager.GetPositionPressed(currentNote.Position))
                {
                    OnLongNoteHit?.Invoke(currentNote, GetHitScore(timeToNote));
                    hittingCurrentLongNote = true;
                    continue;
                }
            }
            
            if (timeToNote < -okayHitWindow)
            {
                OnNoteMissed?.Invoke(currentNote);
                noteIndex++;
            }
        }

        AllNotesRead = true;
        yield return new WaitForSeconds(okayHitWindow * 3);
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
    
    HitScore GetHitScore (double timeToNoteHit)
    {
        double absValue = Math.Abs(timeToNoteHit);
        if (absValue <= perfectHitWindow)
            return HitScore.Perfect;
        if (absValue <= greatHitWindow)
            return HitScore.Great;
        if (absValue <= okayHitWindow)
            return HitScore.Okay;
        return HitScore.Miss;
    }

    void SkipSongStart ()
    {
        if (skippedTime != 0 || !Skippable)
            return;
        Skippable = false;
        float threshold = Mathf.Max(CurrentSongSettings.ApproachRate, SKIP_TO_SECONDS_BEFORE);
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