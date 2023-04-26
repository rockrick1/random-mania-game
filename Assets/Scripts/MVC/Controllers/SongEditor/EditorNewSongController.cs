using System;

public class EditorNewSongController : IDisposable
{
    readonly EditorNewSongView view;
    readonly ISongLoaderModel songLoaderModel;

    public EditorNewSongController (
        EditorNewSongView view,
        ISongLoaderModel songLoaderModel
    )
    {
        this.view = view;
        this.songLoaderModel = songLoaderModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        view.OnCreateSong += HandleCreateSong;
        view.OnOpenFolder += HandleOpenNewSongFolder;
        view.OnEdit += HandleEditNewSong;
    }

    void RemoveListeners ()
    {
        view.OnCreateSong -= HandleCreateSong;
        view.OnOpenFolder -= HandleOpenNewSongFolder;
        view.OnEdit -= HandleEditNewSong;
    }

    public void Open ()
    {
        view.SetCreationState(false);
        view.Open();
    }

    void HandleCreateSong (string songId)
    {
        if (string.IsNullOrWhiteSpace(songId))
        {
            view.ShowError("Please enter a song name.");
            return;
        }
        if (songLoaderModel.SongExists(songId))
        {
            view.ShowError("A song with that name already exists! Pick another name or edit this song's existing file.");
            return;
        }
        songLoaderModel.CreateSongFolder(songId);
        view.SetCreationState(true);
    }

    void HandleOpenNewSongFolder ()
    {
        throw new NotImplementedException();
    }

    void HandleEditNewSong ()
    {
        throw new NotImplementedException();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}