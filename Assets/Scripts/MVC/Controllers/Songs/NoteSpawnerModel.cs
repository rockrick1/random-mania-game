using System;
using System.Collections;
using UnityEngine;

public class NoteSpawnerModel : INoteSpawnerModel
{
    public event Action<Note> OnNoteSpawned;
    
    ISongSettings currentSongSettings;
    Coroutine noteSpawnRoutine;
    
    public void Initialize()
    {
    }

    public void SetSong(ISongSettings currentSongSettings)
    {
        this.currentSongSettings = currentSongSettings;
    }

    public void Play()
    {
        CoroutineRunner.Instance.StartCoroutine(nameof(noteSpawnRoutine), NoteSpawnRoutine());
    }

    IEnumerator NoteSpawnRoutine()
    {
        yield break;
    }
}