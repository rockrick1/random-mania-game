public class GameModel : IGameModel
{
    public GameModel (ISongModel songModel)
    {
        SongModel = songModel;
    }

    public ISongModel SongModel { get; }
    public IInputManager InputManager { get; }

    public void Initialize ()
    {
        SongModel.Initialize();
        SongModel.LoadSong("TearRain");
    }

    public void Dispose ()
    {
        SongModel.Dispose();
    }
}