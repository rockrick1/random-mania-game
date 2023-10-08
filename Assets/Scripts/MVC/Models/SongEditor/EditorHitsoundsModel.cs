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
            float lastNoteTime = GetNextNoteStart();
            float lastNoteEndTime = GetNextNoteEnd();
            
            yield return null;
            
            if (!audioManager.IsPlayingMusic)
                continue;

            float nextNoteTime = GetNextNoteStart();
            float nextNoteEndTime = GetNextNoteEnd();
            
            if (!Mathf.Approximately(lastNoteTime, nextNoteTime) ||
                !Mathf.Approximately(lastNoteEndTime, nextNoteEndTime))
                OnPlayHitsound?.Invoke();
        }
    }

    float GetNextNoteStart ()
    {
        int noteIndex = 0;
        while (noteIndex < songModel.Notes.Count &&
               songModel.Notes[noteIndex].Time + songModel.SongStartingTime < audioManager.MusicTime)
            noteIndex++;
        return noteIndex == songModel.Notes.Count ? -1 : songModel.Notes[noteIndex].Time;
    }

    float GetNextNoteEnd ()
    {
        int noteIndex = 0;
        while (noteIndex < songModel.Notes.Count &&
               songModel.Notes[noteIndex].EndTime + songModel.SongStartingTime < audioManager.MusicTime)
            noteIndex++;
        return noteIndex == songModel.Notes.Count ? -1 : songModel.Notes[noteIndex].EndTime;
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