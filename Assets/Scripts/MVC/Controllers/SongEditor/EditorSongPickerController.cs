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
        RefreshOptions();
    }

    void AddListeners ()
    {
        songLoaderModel.OnSongCreated += HandleSongCreated;
        view.OnSongPicked += HandleSongPicked;
        view.OnOpenFolderClicked += HandleOpenFolderClicked;
        view.OnNewSongClicked += HandleNewSongClicked;
        view.OnRefreshClicked += HandleRefreshClicked;
    }

    void RemoveListeners ()
    {
        songLoaderModel.OnSongCreated -= HandleSongCreated;
        view.OnSongPicked -= HandleSongPicked;
        view.OnOpenFolderClicked -= HandleOpenFolderClicked;
        view.OnNewSongClicked -= HandleNewSongClicked;
        view.OnRefreshClicked -= HandleRefreshClicked;
    }

    void HandleSongCreated () => RefreshOptions();

    void HandleSongPicked (string song)
    {
        model.PickSong(song);
    }

    void HandleOpenFolderClicked ()
    {
        System.Diagnostics.Process.Start("explorer.exe", "/select," + songLoaderModel.SongsPath.Replace(@"/", @"\"));
    }

    void HandleNewSongClicked ()
    {
        //TODO call a popup with text input and instructions here
    }

    void HandleRefreshClicked () => RefreshOptions();

    void RefreshOptions ()
    {
        view.LoadOptions(songLoaderModel.GetAllSongDirs());
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}