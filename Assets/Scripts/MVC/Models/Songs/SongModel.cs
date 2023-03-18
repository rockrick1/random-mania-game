using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongModel : ISongModel
{
    public event Action<Note> OnNoteHit;
    public event Action<Note> OnNoteMissed;
    public event Action OnSongFinished;

    const float HIT_WINDOW = 0.25f;

    public ISongSettings CurrentSongSettings => songLoaderModel.Settings;
    public AudioClip CurrentSongAudio => songLoaderModel.Audio;
    
    readonly INoteSpawnerModel noteSpawnerModel;
    readonly ISongLoaderModel songLoaderModel;

    double elapsed = 0;

    public SongModel(INoteSpawnerModel noteSpawnerModel, ISongLoaderModel songLoaderModel)
    {
        this.noteSpawnerModel = noteSpawnerModel;
        this.songLoaderModel = songLoaderModel;
    }

    public void Initialize()
    {
        noteSpawnerModel.Initialize();
        songLoaderModel.Initialize();
    }

    public void LoadSong(string songId)
    {
        songLoaderModel.LoadSong(songId);
        noteSpawnerModel.SetSong(CurrentSongSettings);
    }

    public void Play()
    {
        noteSpawnerModel.Play();
        CoroutineRunner.Instance.StartCoroutine(nameof(SongRoutine), SongRoutine());
    }

    IEnumerator SongRoutine()
    {
        IReadOnlyList<Note> notes = CurrentSongSettings.Notes;
        TimeSpan hitWindow = TimeSpan.FromSeconds(HIT_WINDOW);
        int notesIndex = 0;
        while (true)
        {
            elapsed += Time.deltaTime;
            TimeSpan noteDistance = TimeSpan.FromSeconds(notes[notesIndex].Timestamp - elapsed);
            if (noteDistance < hitWindow)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log($"note hit! {noteDistance.TotalMilliseconds}");
                    OnNoteHit?.Invoke(notes[notesIndex]);
                    if (++notesIndex >= notes.Count)
                        break;
                    continue;
                }
            }

            if (noteDistance < -hitWindow)
            {
                Debug.Log("MISS! YOU SUCK");
                OnNoteMissed?.Invoke(notes[notesIndex]);
                if (++notesIndex >= notes.Count)
                    break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(hitWindow.Seconds * 3);
        OnSongFinished?.Invoke();
    }

    public void Dispose()
    {
        
    }
}