public class GameModel : IGameModel
{
    public ISongModel SongModel { get; }
    public IComboModel ComboModel { get; }
    public IInputManager InputManager { get; }
    
    public GameModel (ISongModel songModel, IComboModel comboModel, IInputManager inputManager)
    {
        SongModel = songModel;
        ComboModel = comboModel;
        InputManager = inputManager;
    }

    public void Initialize ()
    {
        SongModel.Initialize();
        SongModel.LoadSong("TearRain");
        ComboModel.Initialize();
    }

    public void Dispose ()
    {
        SongModel.Dispose();
        ComboModel.Dispose();
    }
}