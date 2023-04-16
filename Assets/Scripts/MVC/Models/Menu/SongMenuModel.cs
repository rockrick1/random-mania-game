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

    public void Dispose ()
    {
        RemoveListeners();
    }
}