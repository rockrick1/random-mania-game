public class GameModel : IGameModel
{
    public ISongModel SongModel { get; }
    public IScoreModel ScoreModel { get; }
    public IGameInputManager InputManager { get; }
    public IAudioManager AudioManager { get; }

    public GameModel (ISongModel songModel, IScoreModel scoreModel, IGameInputManager inputManager, IAudioManager audioManager)
    {
        SongModel = songModel;
        ScoreModel = scoreModel;
        InputManager = inputManager;
        AudioManager = audioManager;
    }

    public void Initialize ()
    {
        SongModel.Initialize();
        SongModel.LoadSong(GameContext.Current.SelectedSongId);
        ScoreModel.Initialize();
    }

    public void Dispose ()
    {
        SongModel.Dispose();
        ScoreModel.Dispose();
    }
}