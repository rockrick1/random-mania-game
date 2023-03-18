using System;
using System.Collections;
using UnityEngine;

public class NoteSpawnerModel : INoteSpawnerModel
{
    ISongSettings currentSongSettings;
    Coroutine noteSpawnRoutine;
    public event Action<Note> OnNoteSpawned;

    public void Initialize ()
    {
    }

    public void SetSong (ISongSettings currentSongSettings)
    {
        this.currentSongSettings = currentSongSettings;
    }

    public void Play ()
    {
        CoroutineRunner.Instance.StartCoroutine(nameof(noteSpawnRoutine), NoteSpawnRoutine());
    }

    IEnumerator NoteSpawnRoutine ()
    {
        yield break;
    }
}