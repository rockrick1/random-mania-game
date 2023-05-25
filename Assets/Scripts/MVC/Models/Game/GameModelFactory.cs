public static class GameModelFactory
{
    public static GameModel Create (IGameInputManager inputManager, IAudioManager audioManager)
    {
        ISongModel songModel = SongModelFactory.Create(inputManager);
        IPauseModel pauseModel = new PauseModel(songModel);
        IScoreModel scoreModel = new ScoreModel(songModel);
        var model = new GameModel(songModel, pauseModel, scoreModel, inputManager, audioManager);
        return model;
    }
}