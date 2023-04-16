using System.Collections.Generic;

public class SongMenuModel : ISongMenuModel
{
    readonly SongMenuView view;
    readonly ISongLoaderModel songLoaderModel;
    
    
    public SongMenuModel (ISongLoaderModel songLoaderModel)
    {
        this.songLoaderModel = songLoaderModel;
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

    public IReadOnlyList<ISongSettings> GetAllSongs () => songLoaderModel.GetAllSongSettings();

    public void Dispose ()
    {
        RemoveListeners();
    }
}