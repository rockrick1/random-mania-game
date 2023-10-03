using System;

public class SkipSongStartController : IDisposable
{
    readonly SkipSongStartView view;
    readonly ISongModel songModel;

    public SkipSongStartController (
        SkipSongStartView view,
        ISongModel songModel
    )
    {
        this.view = view;
        this.songModel = songModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        songModel.OnSkippableChanged += HandleSkippableChanged;
    }

    void RemoveListeners ()
    {
        songModel.OnSkippableChanged -= HandleSkippableChanged;
    }

    void HandleSkippableChanged (bool skippable)
    {
        if (skippable)
            view.Show();
        else
            view.Hide();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}