public class GameModel : IGameModel
{
    public ISongModel SongModel { get; }
    
    public GameModel(ISongModel songModel)
    {
        SongModel = songModel;
    }

    public void Initialize()
    {
        SongModel.Initialize();
        SongModel.InitializeSong("test");
    }

    public void Dispose()
    {
        SongModel.Dispose();
    }
}