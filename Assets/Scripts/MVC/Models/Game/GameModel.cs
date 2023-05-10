public class GameModel : IGameModel
{
    public ISongModel SongModel { get; }
    public IPauseModel PauseModel { get; }
    public IScoreModel ScoreModel { get; }
    public IGameInputManager InputManager { get; }
    public IAudioManager AudioManager { get; }

    public GameModel (
        ISongModel songModel,
        IPauseModel pauseModel,
        IScoreModel scoreModel,
        IGameInputManager inputManager,
        IAudioManager audioManager
    )
    {
        SongModel = songModel;
        PauseModel = pauseModel;
        ScoreModel = scoreModel;
        InputManager = inputManager;
        AudioManager = audioManager;
    }

    public void Initialize ()
    {
        SongModel.Initialize();
        PauseModel.Initialize();
        SongModel.LoadSong(GameContext.Current.SelectedSongId);
        ScoreModel.Initialize();
    }

    public void Dispose ()
    {
        SongModel.Dispose();
        PauseModel.Dispose();
        ScoreModel.Dispose();
    }
}