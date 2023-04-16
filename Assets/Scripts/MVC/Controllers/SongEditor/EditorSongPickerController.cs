using System;
using System.Collections.Generic;

public class EditorSongPickerController : IDisposable
{
    readonly EditorSongPickerView view;
    readonly IEditorSongPickerModel model;
    readonly ISongLoaderModel songLoaderModel;

    public EditorSongPickerController (
        EditorSongPickerView view,
        IEditorSongPickerModel model,
        ISongLoaderModel songLoaderModel
    )
    {
        this.view = view;
        this.model = model;
        this.songLoaderModel = songLoaderModel;
    }

    public void Initialize ()
    {
        AddListeners();
        view.LoadOptions(songLoaderModel.GetAllSongDirs());
    }

    void AddListeners ()
    {
        view.OnSongPicked += HandleSongPicked;
    }

    void RemoveListeners ()
    {
        view.OnSongPicked -= HandleSongPicked;
    }

    void HandleSongPicked (string song)
    {
        model.PickSong(song);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}