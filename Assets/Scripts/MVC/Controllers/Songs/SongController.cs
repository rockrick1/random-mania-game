public class SongController
{
    public NoteSpawnerController NoteSpawnerController;
    
    readonly SongView view;
    readonly ISongModel model;

    public SongController(SongView view, ISongModel model)
    {
        this.view = view;
        this.model = model;
    }

    public void Initialize()
    {
        
    }
}