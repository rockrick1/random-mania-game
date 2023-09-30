using System;
using System.Globalization;

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

    public void Open () => view.Open();
    
    public void Close () => view.Close();

    void SyncSongInfoBox ()
    {
        view.SetSelectedSongTitle(model.SelectedSongSettings.Title);
        view.SetSelectedSongArtist(model.SelectedSongSettings.Artist);
        view.SetSelectedSongDifficultyName(model.SelectedSongSettings.DifficultyName);
        view.SetSelectedSongBPM(model.SelectedSongSettings.Bpm.ToString(CultureInfo.InvariantCulture));
        view.SetSelectedSongApproachRate(model.SelectedSongSettings.ApproachRate.ToString(CultureInfo.InvariantCulture));
        view.SetSelectedSongDifficulty(model.SelectedSongSettings.Difficulty.ToString(CultureInfo.InvariantCulture));
    }

    void AddListeners ()
    {
        view.OnSongClicked += HandleSongClicked;
        view.OnBackPressed += HandleBackPressed;
        view.OnPlayPressed += HandlePlayPressed;
    }

    void RemoveListeners ()
    {
        view.OnSongClicked -= HandleSongClicked;
        view.OnBackPressed -= HandleBackPressed;
        view.OnPlayPressed -= HandlePlayPressed;
    }

    void HandleSongClicked (string songId, string songDifficultyName)
    {
        model.PickSong(songId, songDifficultyName);
        SyncSongInfoBox();
    }

    void HandleBackPressed () => OnBackPressed?.Invoke();

    void HandlePlayPressed () => model.EnterGame();

    public void Dispose ()
    {
        model.Dispose();
        RemoveListeners();
    }
}