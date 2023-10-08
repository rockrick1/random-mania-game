using System;
using System.Collections;
using UnityEngine;

public class EditorHitsoundsModel : IEditorHitsoundsModel
{
    public event Action OnPlayHitsound;

    readonly IEditorSongModel songModel;
    readonly IAudioManager audioManager;
    
    public EditorHitsoundsModel (
        IEditorSongModel songModel,
        IAudioManager audioManager
    )
    {
        this.songModel = songModel;
        this.audioManager = audioManager;
    }

    public void Initialize ()
    {
        AddListeners();
        CoroutineRunner.Instance.StartRoutine(nameof(HitsoundsRoutine), HitsoundsRoutine());
    }

    IEnumerator HitsoundsRoutine()
    {
        while (true)
        {
            float lastNoteTime = GetNextNoteTime();
            
            yield return null;
            
            if (!audioManager.IsPlayingMusic)
                continue;

            float nextNoteTime = GetNextNoteTime();
            if (!Mathf.Approximately(lastNoteTime, nextNoteTime))
                OnPlayHitsound?.Invoke();
        }
    }

    float GetNextNoteTime ()
    {
        int noteIndex = 0;
        while (noteIndex < songModel.Notes.Count &&
               songModel.Notes[noteIndex].Time + songModel.SongStartingTime < audioManager.MusicTime)
            noteIndex++;
        return noteIndex == songModel.Notes.Count ? -1 : songModel.Notes[noteIndex].Time;
    }

    void AddListeners ()
    {
    }

    void RemoveListeners ()
    {
    }

    public void Dispose ()
    {
        RemoveListeners();
        CoroutineRunner.Instance.StopRoutine(nameof(HitsoundsRoutine));
    }
}