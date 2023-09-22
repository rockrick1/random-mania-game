using System.Collections.Generic;

public class SongMenuModel : ISongMenuModel
{
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