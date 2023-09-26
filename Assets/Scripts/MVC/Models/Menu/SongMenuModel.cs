using System;
using System.Collections.Generic;

public class SongMenuModel : ISongMenuModel
{
    public event Action<string, string> OnSongSelected;
    
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

    public void PickSong(string songId, string songDifficultyName) => OnSongSelected?.Invoke(songId, songDifficultyName);

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