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
        NoteCreationResult? result = model.ButtonClicked(pos, view.SongPlayer.time, view.Height);
        if (result == null)
            return;
        if (result.Value.Substituted)
            view.RemoveNote(result.Value.Index);
        view.CreateNote(result.Value.Note, result.Value.Index);
    }

    void HandleSongLoaded ()
    {
        view.ClearObjects();
        view.SetupSong(songLoaderModel.Settings, songLoaderModel.Audio.length);
        CreateNotes();
        CreateHorizontalSeparators(model.SelectedSignature);
    }

    void CreateNotes ()
    {
        foreach (Note note in songLoaderModel.Settings.Notes)
            view.CreateNote(note);
    }

    void CreateHorizontalSeparators (int signature)
    {
        float beatInterval = 60f / songLoaderModel.Settings.Bpm;
        int i = 0;
        for (float t = songLoaderModel.Settings.StartingTime; t < songLoaderModel.Audio.length; t += beatInterval / signature, i++)
        {
            view.CreateSeparator(model.GetSeparatorColorByIndex(i, signature));
        }
        view.ChangeSeparatorsDistance(model.SelectedSignature);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}