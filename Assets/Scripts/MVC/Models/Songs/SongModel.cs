using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongModel : ISongModel
{
    public event Action<Note> OnNoteSpawned;
    public event Action<Note, HitScore> OnNoteHit;
    public event Action<Note> OnNoteMissed;
    public event Action OnAudioStartTimeReached;
    public event Action OnSongFinished;

    public ISongSettings CurrentSongSettings => songLoaderModel.Settings;
    public AudioClip CurrentSongAudio => songLoaderModel.Audio;

    readonly IInputManager inputManager;
    readonly ISongLoaderModel songLoaderModel;
    
    float perfectHitWindow;
    float greatHitWindow;
    float okayHitWindow;

    public SongModel (IInputManager inputManager, ISongLoaderModel songLoaderModel)
    {
        this.inputManager = inputManager;
        this.songLoaderModel = songLoaderModel;
    }

    public void Initialize ()
    {
        songLoaderModel.Initialize();
    }

    public void LoadSong (string songId)
    {
        songLoaderModel.LoadSong(songId);
        perfectHitWindow = (80 - 6 * CurrentSongSettings.Difficulty) / 1000f;
        greatHitWindow = (140 - 8 * CurrentSongSettings.Difficulty) / 1000f;
        okayHitWindow = (200 - 10 * CurrentSongSettings.Difficulty) / 1000f;
    }

    public void Play ()
    {
        CoroutineRunner.Instance.StartCoroutine(nameof(AudioStartRoutine), AudioStartRoutine());
        CoroutineRunner.Instance.StartCoroutine(nameof(SongRoutine), SongRoutine());
        CoroutineRunner.Instance.StartCoroutine(nameof(NoteSpawnRotutine), NoteSpawnRotutine());
        // CoroutineRunner.Instance.StartCoroutine(nameof(TestRoutine), TestRoutine());
    }

    IEnumerator AudioStartRoutine ()
    {
        yield return new WaitForSeconds(CurrentSongSettings.ApproachRate);
        OnAudioStartTimeReached?.Invoke();
    }

    IEnumerator NoteSpawnRotutine ()
    {
        IReadOnlyList<Note> notes = CurrentSongSettings.Notes;
        int noteIndex = 0;
        double elapsed = GetStartingElapsed();

        while (true)
        {
            yield return null;
            
            elapsed += Time.deltaTime;
            double noteSpawnTime = notes[noteIndex].Timestamp - CurrentSongSettings.ApproachRate;
            if (elapsed > noteSpawnTime)
            {
                OnNoteSpawned?.Invoke(notes[noteIndex]);
                if (++noteIndex >= notes.Count)
                    break;
            }
        }
    }

    IEnumerator SongRoutine ()
    {
        IReadOnlyList<Note> notes = CurrentSongSettings.Notes;
        int noteIndex = 0;
        double elapsed = GetStartingElapsed();
        
        while (true)
        {
            yield return null;
            
            elapsed += Time.deltaTime;
            double timeToNoteHit = notes[noteIndex].Timestamp - elapsed;
            if (timeToNoteHit < okayHitWindow)
            {
                if (inputManager.GetPositionPressed(notes[noteIndex].Position))
                {
                    OnNoteHit?.Invoke(notes[noteIndex], GetHitScrore(Math.Abs(timeToNoteHit)));
                    if (++noteIndex >= notes.Count)
                        break;
                    continue;
                }
            }

            if (timeToNoteHit < -okayHitWindow)
            {
                OnNoteMissed?.Invoke(notes[noteIndex]);
                if (++noteIndex >= notes.Count)
                    break;
            }
        }

        yield return new WaitForSeconds(okayHitWindow * 3);
        OnSongFinished?.Invoke();
    }

    IEnumerator TestRoutine ()
    {
        yield return new WaitForSeconds(CurrentSongSettings.StartingTime);
        
        IReadOnlyList<Note> notes = CurrentSongSettings.Notes;
        double elapsed = 0;

        int i = 0;
        while (true)
        {
            yield return null;
            
            elapsed += Time.deltaTime;

            if (elapsed > notes[i].Timestamp)
            {
                Debug.LogError("BABOOEY");
                i++;
            }
        }
    }

    double GetStartingElapsed () => CurrentSongSettings.StartingTime < CurrentSongSettings.ApproachRate
        ? -2 * CurrentSongSettings.ApproachRate + CurrentSongSettings.StartingTime
        : -CurrentSongSettings.ApproachRate - CurrentSongSettings.StartingTime;

    HitScore GetHitScrore (double timeToNoteHit)
    {
        if (timeToNoteHit <= perfectHitWindow)
            return HitScore.Perfect;
        if (timeToNoteHit <= greatHitWindow)
            return HitScore.Great;
        if (timeToNoteHit <= okayHitWindow)
            return HitScore.Okay;
        return HitScore.Miss;
    }

    public void Dispose ()
    {
    }
}