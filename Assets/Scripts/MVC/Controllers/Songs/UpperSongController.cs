using System;

public class UpperSongController : IDisposable
{
    readonly ISongModel songModel;
    readonly UpperSongView view;

    public UpperSongController (UpperSongView view, ISongModel songModel)
    {
        this.view = view;
        this.songModel = songModel;
    }

    public void Initialize ()
    {
        Addlisteners();
        view.SetApproachRate(songModel.CurrentSongSettings.ApproachRate);
    }

    void Addlisteners ()
    {
        songModel.OnNoteSpawned += HandleNoteSpawned;
    }

    void RemoveListeners ()
    {
        songModel.OnNoteSpawned -= HandleNoteSpawned;
    }

    void HandleNoteSpawned (Note note) => view.SpawnNote(note);

    public void Dispose ()
    {
        RemoveListeners();
    }
}