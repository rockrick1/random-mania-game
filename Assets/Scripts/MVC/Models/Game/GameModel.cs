public class GameModel : IGameModel
{
    public ISongModel SongModel { get; }
    public IComboModel ComboModel { get; }
    public IGameInputManager InputManager { get; }
    public IAudioManager AudioManager { get; }

    public GameModel (ISongModel songModel, IComboModel comboModel, IGameInputManager inputManager, IAudioManager audioManager)
    {
        SongModel = songModel;
        ComboModel = comboModel;
        InputManager = inputManager;
        AudioManager = audioManager;
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