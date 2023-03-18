public class SongModel : ISongModel
{
    INoteSpawnerModel NoteSpawnerModel;
    ISongProcessorModel SongProcessorModel;
    
    public SongModel()
    {
        NoteSpawnerModel = new NoteSpawnerModel();
        
    }

    public void Initialize()
    {
        NoteSpawnerModel.Initialize();
        SongProcessorModel.Initialize();
    }

    public void InitializeSong(string song)
    {
        SongProcessorModel.InitializeSong(song);
    }

    public void Dispose()
    {
        
    }
}