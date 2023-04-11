using System;

public class EditorSongController : IDisposable
{
    readonly EditorSongView view;
    readonly IEditorSongModel model;
    readonly ISongLoaderModel songLoaderModel;

    public EditorSongController (EditorSongView view, IEditorSongModel model, ISongLoaderModel songLoaderModel)
    {
        this.view = view;
        this.model = model;
        this.songLoaderModel = songLoaderModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        view.OnFieldButtonClicked += HandleFieldButtonClicked;
        songLoaderModel.OnSongLoaded += HandleSongLoaded;
    }

    void RemoveListeners ()
    {
        view.OnFieldButtonClicked -= HandleFieldButtonClicked;
        songLoaderModel.OnSongLoaded -= HandleSongLoaded;
    }

    void HandleFieldButtonClicked (int pos)
    {
        model.ButtonClicked(pos);
    }

    void HandleSongLoaded ()
    {
        view.SetupSong(songLoaderModel.Settings, songLoaderModel.Audio.length);
        CreateNotes();
        CreateHorizontalSeparators();
    }

    void CreateNotes ()
    {
        foreach (Note note in songLoaderModel.Settings.Notes)
            view.SpawnNote(note);
    }

    void CreateHorizontalSeparators ()
    {
        view.SetStartingTime(songLoaderModel.Settings.StartingTime);
        double beatInterval = 60f / songLoaderModel.Settings.Bpm;
        int i = 0;
        for (double t = songLoaderModel.Settings.StartingTime; t < songLoaderModel.Audio.length; t += beatInterval / 2)
        {
            view.CreateSeparator();
            i++;
        }
    }

    void ChangeSeparatorsDistance (int interval) => view.ChangeSeparatorsDistance(interval);

    public void Dispose ()
    {
        RemoveListeners();
    }
}