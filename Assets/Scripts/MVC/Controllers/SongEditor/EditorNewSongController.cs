using System;

public class EditorNewSongController : IDisposable
{
    public event Action<string, string> OnEditNewSong;
    
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

    void HandleCreateSong (string songName, string artistName, string songDifficultyName)
    {
        if (string.IsNullOrWhiteSpace(songName))
        {
            view.ShowError("Please enter a song name.");
            return;
        }
        if (string.IsNullOrWhiteSpace(artistName))
        {
            view.ShowError("Please enter an artist name.");
            return;
        }
        if (string.IsNullOrWhiteSpace(songDifficultyName))
        {
            view.ShowError("Please enter a difficulty name.");
            return;
        }
        if (model.SongExists(songName, artistName))
        {
            view.ShowError("A song with that name already exists! Pick another name or edit this song's existing file.");
            return;
        }
        model.CreateSong(songName, artistName, songDifficultyName);
        view.SetCreationState(true);
    }

    void HandleOpenNewSongFolder () => model.OpenSongFolder();

    void HandleEditNewSong () => OnEditNewSong?.Invoke(model.LastCreatedSongId, model.LastCreatedSongDifficultyName);

    public void Dispose ()
    {
        RemoveListeners();
    }
}