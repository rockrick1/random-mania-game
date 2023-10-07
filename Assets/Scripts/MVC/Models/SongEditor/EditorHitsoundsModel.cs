using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorHitsoundsModel : IEditorHitsoundsModel
{
    public event Action OnPlayHitsound;

    readonly IEditorSongModel songModel;
    
    public EditorHitsoundsModel (
        IEditorSongModel songModel
    )
    {
        this.songModel = songModel;
    }

    public void Initialize ()
    {
        AddListeners();
        CoroutineRunner.Instance.StartRoutine(nameof(HitsoundsRoutine), HitsoundsRoutine());
    }

    IEnumerator HitsoundsRoutine ()
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
        
        while (true)
        {
            yield return null;
            if (audioManager.IsPlayingMusic)
                continue;
            Note nextNote = model.Notes.First(x => x.Note.Time > ).Note;
            audioManager.MusicTime;
        }
    }

    void AddListeners ()
    {
        inputManager.OnSavePressed += HandleSavePressed;
    }

    void RemoveListeners ()
    {
        inputManager.OnSavePressed -= HandleSavePressed;
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}