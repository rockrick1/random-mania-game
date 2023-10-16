using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class SongMenuModel : ISongMenuModel
{
    public event Action<ISongSettings> OnSongSelected;
    
    public ISongSettings SelectedSongSettings { get; private set; }

    readonly SongMenuView view;
    readonly SongLoader songLoader;

    public SongMenuModel (SongLoader songLoader)
    {
        this.songLoader = songLoader;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    public void PickFirstSong ()
    {
        ISongSettings firstSong = songLoader.SongsCache.First().Value.First().Value;
        PickSong(firstSong.Id, firstSong.DifficultyName);
    }

    public bool PickSong (string songId, string songDifficultyName)
    {
        if (SelectedSongSettings == songLoader.SongsCache[songId][songDifficultyName])
            return false;
        GameContext.Current.SelectedSongId = songId;
        GameContext.Current.SelectedSongDifficulty = songDifficultyName;
        SelectedSongSettings = songLoader.SongsCache[songId][songDifficultyName];
        OnSongSelected?.Invoke(SelectedSongSettings);
        return true;
    }

    public void EnterGame ()
    {
        SceneManager.LoadScene("Game");
    }

    public void SetAR (float value)
    {
        double roundedValue = Math.Round(value, 1);
        GameContext.Current.ApproachRate = (float)roundedValue;
    }

    void AddListeners ()
    {
    }

    void RemoveListeners ()
    {
    }

    public IReadOnlyList<ISongSettings> GetAllSongs () => songLoader.GetAllSongSettings();

    public void Dispose ()
    {
        RemoveListeners();
    }
}