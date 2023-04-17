using System;

public class EditorSongPickerController : IDisposable
{
    readonly EditorSongPickerView view;
    readonly NewSongView newSongView;
    readonly IEditorSongPickerModel model;
    readonly ISongLoaderModel songLoaderModel;

    public EditorSongPickerController (
        EditorSongPickerView view,
        NewSongView newSongView,
        IEditorSongPickerModel model,
        ISongLoaderModel songLoaderModel
    )
    {
        this.view = view;
        this.newSongView = newSongView;
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
        newSongView.OnCreateSong += HandleCreateSong;
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

    void HandleNewSongClicked () => newSongView.Open();

    void HandleRefreshClicked () => RefreshOptions();

    void HandleCreateSong (string songId)
    {
        if (songLoaderModel.SongExists(songId))
        {
            view.ShowError("A song with that name was already created! Pick another name or editr this song's existing file.");
            return;
        }
        songLoaderModel.CreateSongFolder(songId);
        newSongView.Close();
    }

    void RefreshOptions () => view.LoadOptions(songLoaderModel.GetAllSongDirs());

    public void Dispose ()
    {
        RemoveListeners();
    }
}