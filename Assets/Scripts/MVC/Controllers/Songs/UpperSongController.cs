using System;
using System.Collections.Generic;

public class UpperSongController : IDisposable
{
    readonly ISongModel songModel;
    readonly IScoreModel scoreModel;
    readonly UpperSongView view;
    readonly SongController songController;
    readonly LowerSongController lowerSongController;
    
    readonly List<BaseNoteView> liveNotes = new();
    
    float noteSpeed;

    public UpperSongController (
        UpperSongView view,
        LowerSongController lowerSongController,
        ISongModel songModel,
        IScoreModel scoreModel
    )
    {
        this.view = view;
        this.lowerSongController = lowerSongController;
        this.songModel = songModel;
        this.scoreModel = scoreModel;
    }

    public void Initialize ()
    {
        Addlisteners();
        noteSpeed = (view.SpawnerYPos - lowerSongController.HitterYPos) / songModel.ApproachRate;
    }

    void Addlisteners ()
    {
        songModel.OnNoteSpawned += HandleNoteSpawned;
        songModel.OnNoteMissed += HandleNoteMissed;
        songModel.OnNoteHit += HandleNoteHit;
        songModel.OnLongNoteReleased += HandleLongNoteReleased;
    }

    void RemoveListeners ()
    {
        songModel.OnNoteSpawned -= HandleNoteSpawned;
        songModel.OnNoteMissed -= HandleNoteMissed;
        songModel.OnNoteHit -= HandleNoteHit;
        songModel.OnLongNoteReleased -= HandleLongNoteReleased;
    }

    void HandleNoteSpawned (Note note)
    {
        if (note.IsLong)
        {
            float height = (note.EndTime - note.Time) * noteSpeed;
            liveNotes.Add(view.SpawnLongNote(note, noteSpeed, height));
        }
        else
            liveNotes.Add(view.SpawnNote(note, noteSpeed));
    }

    void HandleNoteHit (Note note, double timeToNote)
    {
        HitScore score = scoreModel.GetHitScore(timeToNote);
        view.ShowHitFeedback(score);
        
        for (int i = 0; i < liveNotes.Count; i++)
        {
            if (liveNotes[i].Note != note)
                continue;
            liveNotes[i].HitAnimation();
            liveNotes.RemoveAt(i);
            return;
        }
    }

    void HandleLongNoteReleased (Note note, double timeToNote) => HandleNoteHit(note, timeToNote);

    void HandleNoteMissed (Note note)
    {
        view.ShowHitFeedback(HitScore.Miss);
        
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