using System;

public class EditorSongController : IDisposable
{
    readonly EditorSongView view;
    readonly EditorSongDetailsController songDetailsController;
    readonly IEditorSongModel model;
    readonly ISongLoaderModel songLoaderModel;

    public EditorSongController (
        EditorSongView view,
        EditorSongDetailsController songDetailsController,
        IEditorSongModel model,
        ISongLoaderModel songLoaderModel
    )
    {
        this.view = view;
        this.songDetailsController = songDetailsController;
        this.model = model;
        this.songLoaderModel = songLoaderModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        view.OnFieldButtonLeftClicked += HandleFieldButtonLeftClicked;
        view.OnFieldButtonRightClicked += HandleFieldButtonRightClicked;
        songDetailsController.OnSetStartingTimeClicked += HandleSetStartingTime;
        songDetailsController.OnApplyClicked += HandleApplySongDetails;
        songDetailsController.OnSignatureChanged += HandleSignatureChanged;
        songLoaderModel.OnSongLoaded += HandleSongLoaded;
    }

    void RemoveListeners ()
    {
        view.OnFieldButtonLeftClicked -= HandleFieldButtonLeftClicked;
        view.OnFieldButtonRightClicked -= HandleFieldButtonRightClicked;
        songDetailsController.OnSignatureChanged -= HandleSignatureChanged;
        songLoaderModel.OnSongLoaded -= HandleSongLoaded;
    }

    void HandleFieldButtonLeftClicked (int pos)
    {
        NoteCreationResult? result = model.ButtonLeftClicked(pos, view.SongPlayer.time, view.Height);
        if (result == null)
            return;
        if (result.Value.Substituted)
            view.RemoveNote(result.Value.Index);
        view.CreateNote(result.Value.Note, result.Value.Index);
    }

    void HandleFieldButtonRightClicked (int pos)
    {
        int result = model.ButtonRightClicked(pos, view.SongPlayer.time, view.Height);
        if (result == -1)
            return;
        view.RemoveNote(result);
    }

    void HandleSetStartingTime ()
    {
        throw new NotImplementedException();
    }

    void HandleApplySongDetails (float bpm, float ar, float diff, float startingTime)
    {
        model.ChangeBpm(bpm);
        model.ChangeAr(ar);
        model.ChangeDiff(diff);
        model.ChangeStartingTime(startingTime);
        HandleSongLoaded();
    }

    void HandleSignatureChanged (int signature)
    {
        if (model.SelectedSignature == signature)
            return;
        model.ChangeSignature(signature);
        view.ClearSeparators();
        CreateHorizontalSeparators();
    }

    void HandleSongLoaded ()
    {
        view.ClearNotes();
        view.ClearSeparators();
        view.SetupSong(songLoaderModel.Settings, songLoaderModel.Audio.length);
        CreateNotes();
        CreateHorizontalSeparators();
    }

    void CreateNotes ()
    {
        foreach (Note note in songLoaderModel.Settings.Notes)
            view.CreateNote(note);
    }

    void CreateHorizontalSeparators ()
    {
        int i = 0;
        for (float t = songLoaderModel.Settings.StartingTime;
             t < songLoaderModel.Audio.length;
             t += model.SignedBeatInterval, i++)
        {
            view.CreateSeparator(model.GetSeparatorColorByIndex(i));
        }
        view.ChangeSeparatorsDistance(model.SelectedSignature);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}