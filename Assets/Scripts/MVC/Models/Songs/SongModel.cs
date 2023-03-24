using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongModel : ISongModel
{
    const float SONG_START_INTERVAL = 1f;
    const float HIT_WINDOW = 0.25f;

    public event Action<Note> OnNoteSpawned;
    public event Action<Note> OnNoteHit;
    public event Action<Note> OnNoteMissed;
    public event Action OnSongFinished;

    public ISongSettings CurrentSongSettings => songLoaderModel.Settings;
    public AudioClip CurrentSongAudio => songLoaderModel.Audio;

    readonly IInputManager inputManager;
    readonly ISongLoaderModel songLoaderModel;

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
    }

    public void Play ()
    {
        CoroutineRunner.Instance.StartCoroutine(nameof(SongRoutine), SongRoutine());
        CoroutineRunner.Instance.StartCoroutine(nameof(NoteSpawnRotutine), NoteSpawnRotutine());
        // CoroutineRunner.Instance.StartCoroutine(nameof(TestRoutine), TestRoutine());
    }

    IEnumerator TestRoutine ()
    {
        yield return new WaitForSeconds(SONG_START_INTERVAL);
        
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

    IEnumerator NoteSpawnRotutine ()
    {
        yield return new WaitForSeconds(SONG_START_INTERVAL);
        
        IReadOnlyList<Note> notes = CurrentSongSettings.Notes;
        int noteIndex = 0;
        double elapsed = 0;

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
        yield return new WaitForSeconds(SONG_START_INTERVAL);
        
        IReadOnlyList<Note> notes = CurrentSongSettings.Notes;
        int noteIndex = 0;
        double elapsed = 0;
        
        while (true)
        {
            yield return null;
            
            elapsed += Time.deltaTime;
            double timeToNoteHit = notes[noteIndex].Timestamp - elapsed;
            if (timeToNoteHit < HIT_WINDOW)
            {
                if (inputManager.GetPositionPressed(notes[noteIndex].Position))
                {
                    Debug.Log($"note hit! {(int)(timeToNoteHit * 1000f)}");
                    OnNoteHit?.Invoke(notes[noteIndex]);
                    if (++noteIndex >= notes.Count)
                        break;
                    continue;
                }
            }

            if (timeToNoteHit < -HIT_WINDOW)
            {
                Debug.Log("MISS! YOU SUCK");
                OnNoteMissed?.Invoke(notes[noteIndex]);
                if (++noteIndex >= notes.Count)
                    break;
            }
        }

        yield return new WaitForSeconds(HIT_WINDOW * 3);
        OnSongFinished?.Invoke();
    }

    public void Dispose ()
    {
    }
}