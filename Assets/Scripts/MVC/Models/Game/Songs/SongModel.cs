using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongModel : ISongModel
{
    const float SKIP_TO_SECONDS_BEFORE = 1.5f;
    public event Action<Note> OnNoteSpawned;
    public event Action<Note, HitScore> OnNoteHit;
    public event Action<Note, HitScore> OnLongNoteHit;
    public event Action<Note, HitScore> OnLongNoteReleased;
    public event Action<Note> OnNoteMissed;
    public event Action OnAudioStartTimeReached;
    public event Action<float> OnSongStartSkipped;
    public event Action OnSongFinished;

    public SongLoader SongLoaderModel { get; }
    public ISongSettings CurrentSongSettings => SongLoaderModel.GetSelectedSongSettings();
    public bool AllNotesRead { get; private set; }

    readonly IGameInputManager inputManager;
    
    float perfectHitWindow;
    float greatHitWindow;
    float okayHitWindow;

    double dspSongStart;
    double pauseOffset;
    float skippedTime;

    public SongModel (IGameInputManager inputManager, SongLoader songLoaderModel)
    {
        this.inputManager = inputManager;
        SongLoaderModel = songLoaderModel;
    }

    public void Initialize ()
    {
        // SongLoaderModel.Initialize();
        AddListeners();
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

    public void LoadSong (string songId, string songDifficultyName)
    {
        SongLoaderModel.SelectedSongId = songId;
        SongLoaderModel.SelectedSongDifficulty = songDifficultyName;
        perfectHitWindow = (80 - 6 * CurrentSongSettings.Difficulty) / 1000f;
        greatHitWindow = (140 - 8 * CurrentSongSettings.Difficulty) / 1000f;
        okayHitWindow = (200 - 10 * CurrentSongSettings.Difficulty) / 1000f;
    }

    public void Play ()
    {
        dspSongStart = AudioSettings.dspTime + GetStartingElapsed();
        
        CoroutineRunner.Instance.StartRoutine(nameof(AudioStartRoutine), AudioStartRoutine());
        CoroutineRunner.Instance.StartRoutine(nameof(NoteSpawnRotutine), NoteSpawnRotutine());
        CoroutineRunner.Instance.StartRoutine(nameof(NotesHitRoutine), NotesHitRoutine());
        CoroutineRunner.Instance.StartRoutine(nameof(PauseOffsetRoutine), PauseOffsetRoutine());
    }

    double GetStartingElapsed () => CurrentSongSettings.StartingTime < CurrentSongSettings.ApproachRate
        ? CurrentSongSettings.ApproachRate + CurrentSongSettings.StartingTime
        : CurrentSongSettings.StartingTime;

    IEnumerator AudioStartRoutine ()
    {
        if (CurrentSongSettings.ApproachRate > CurrentSongSettings.StartingTime)
            yield return new WaitForSeconds(CurrentSongSettings.ApproachRate);
        OnAudioStartTimeReached?.Invoke();
    }

    IEnumerator NoteSpawnRotutine ()
    {
        IReadOnlyList<Note> notes = CurrentSongSettings.Notes;
        int noteIndex = 0;
        while (true)
        {
            yield return null;
            
            double elapsed = AudioSettings.dspTime - dspSongStart - pauseOffset + skippedTime;
            double noteSpawnTime = notes[noteIndex].Time - CurrentSongSettings.ApproachRate;
            
            if (elapsed > noteSpawnTime)
            {
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

            double elapsed = AudioSettings.dspTime - dspSongStart - pauseOffset + skippedTime;
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
                    // Debug.Log($"{DateTime.Now}");
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
        float threshold = Mathf.Max(CurrentSongSettings.ApproachRate, SKIP_TO_SECONDS_BEFORE);
        skippedTime = CurrentSongSettings.Notes[0].Time - threshold;
        OnSongStartSkipped?.Invoke(skippedTime);
    }

    public void Dispose ()
    {
        RemoveListeners();
        CoroutineRunner.Instance.StopRoutine(nameof(AudioStartRoutine));
        CoroutineRunner.Instance.StopRoutine(nameof(NoteSpawnRotutine));
        CoroutineRunner.Instance.StopRoutine(nameof(NotesHitRoutine));
        CoroutineRunner.Instance.StopRoutine(nameof(PauseOffsetRoutine));
    }
}