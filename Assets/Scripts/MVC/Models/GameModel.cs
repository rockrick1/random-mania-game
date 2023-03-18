public class GameModel : IGameModel
{
    public ISongModel SongModel { get; }
    public IInputManager InputManager { get; }
    
    public GameModel (ISongModel songModel, IInputManager inputManager)
    {
        SongModel = songModel;
        InputManager = inputManager;
    }

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