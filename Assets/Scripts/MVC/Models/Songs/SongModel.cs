﻿using System;
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

    readonly ISongLoaderModel songLoaderModel;

    public SongModel (INoteSpawnerModel noteSpawnerModel, ISongLoaderModel songLoaderModel)
    {
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
        float milisForNoteToFall = CurrentSongSettings.ApproachRate * 1000f;
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
            double noteHitTime = notes[noteIndex].Timestamp;
            if (elapsed > noteHitTime - HIT_WINDOW)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log($"note hit! {elapsed - noteHitTime}");
                    OnNoteHit?.Invoke(notes[noteIndex]);
                    if (++noteIndex >= notes.Count)
                        break;
                    continue;
                }
            }

            if (elapsed > noteHitTime + HIT_WINDOW)
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