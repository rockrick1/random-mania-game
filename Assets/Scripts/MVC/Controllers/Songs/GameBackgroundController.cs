using System;
using UnityEngine.SceneManagement;

public class GameBackgroundController : IDisposable
{
    public event Action OnRetry;
    public event Action OnQuit;

    readonly GameBackgroundView view;
    readonly SongLoader songLoader;

    public GameBackgroundController (
        GameBackgroundView view,
        SongLoader songLoader
    )
    {
        this.view = view;
        this.songLoader = songLoader;
    }

    public void Initialize ()
    {
        AddListeners();
        SetCurrentSongBackground();
    }

    void AddListeners ()
    {
    }

    void RemoveListeners ()
    {
    }

    void SetCurrentSongBackground()
    {
        view.SetBackground(
            songLoader.SongsCache[GameContext.Current.SelectedSongId][GameContext.Current.SelectedSongDifficulty]
                .Background);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}