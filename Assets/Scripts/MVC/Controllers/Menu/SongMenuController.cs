using System;

public class SongMenuController : IDisposable
{
    public event Action OnBackPressed;
    
    readonly SongMenuView view;
    readonly ISongMenuModel model;
    
    public SongMenuController (SongMenuView view, ISongMenuModel model)
    {
        this.view = view;
        this.model = model;
    }

    public void Initialize ()
    {
        AddListeners();
        view.Setup(model.GetAllSongs());
    }

    void AddListeners ()
    {
        view.OnSongClicked += HandleSongClicked;
        view.OnBackPressed += HandleBackPressed;
    }

    void RemoveListeners ()
    {
        view.OnSongClicked -= HandleSongClicked;
        view.OnBackPressed -= HandleBackPressed;
    }

    void HandleSongClicked (string songId, string songDifficultyName) => model.PickSong(songId, songDifficultyName);
    
    void HandleBackPressed () => OnBackPressed?.Invoke();

    public void Open () => view.Open();
    
    public void Close () => view.Close();

    public void Dispose ()
    {
        model.Dispose();
        RemoveListeners();
    }
}