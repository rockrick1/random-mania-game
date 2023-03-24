using System;
using System.Collections.Generic;

public class UpperSongController : IDisposable
{
    readonly ISongModel songModel;
    readonly UpperSongView view;
    readonly LowerSongController lowerSongController;
    
    List<NoteView> liveNotes = new();
    float noteSpeed;

    public UpperSongController (UpperSongView view, LowerSongController lowerSongController, ISongModel songModel)
    {
        this.view = view;
        this.songModel = songModel;
        this.lowerSongController = lowerSongController;
    }

    public void Initialize ()
    {
        Addlisteners();
        noteSpeed = (view.SpawnPoints[0].transform.position.y - lowerSongController.HitterYPos) /
                    songModel.CurrentSongSettings.ApproachRate;
    }

    void Addlisteners ()
    {
        songModel.OnNoteSpawned += HandleNoteSpawned;
        songModel.OnNoteMissed += HandleNoteMissed;
        songModel.OnNoteHit += HandleNoteHit;
    }

    void RemoveListeners ()
    {
        songModel.OnNoteSpawned -= HandleNoteSpawned;
        songModel.OnNoteMissed -= HandleNoteMissed;
        songModel.OnNoteHit -= HandleNoteHit;
    }

    void HandleNoteSpawned (Note note) => liveNotes.Add(view.SpawnNote(note, noteSpeed));

    void HandleNoteHit (Note note)
    {
        for (int i = 0; i < liveNotes.Count; i++)
        {
            if (liveNotes[i].Note != note)
                continue;
            liveNotes[i].HitAnimation();
            liveNotes.RemoveAt(i);
            return;
        }
    }

    void HandleNoteMissed (Note note)
    {
        foreach (var noteView in liveNotes)
        {
            if (noteView.Note != note)
                continue;
            noteView.Destroy();
            return;
        }
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}