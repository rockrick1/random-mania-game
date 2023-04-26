using System;

public class EditorNewSongController : IDisposable
{
    public event Action<string> OnEditNewSong;
    
    readonly EditorNewSongView view;
    readonly IEditorNewSongModel model;

    public EditorNewSongController (
        EditorNewSongView view,
        IEditorNewSongModel model
    )
    {
        this.view = view;
        this.model = model;
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
        if (model.SongExists(songId))
        {
            view.ShowError("A song with that name already exists! Pick another name or edit this song's existing file.");
            return;
        }
        model.CreateSongFolder(songId);
        view.SetCreationState(true);
    }

    void HandleOpenNewSongFolder () => model.OpenSongFolder();

    void HandleEditNewSong () => OnEditNewSong?.Invoke(model.LastCreatedSongId);

    public void Dispose ()
    {
        RemoveListeners();
    }
}