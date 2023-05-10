public static class GameModelFactory
{
    public static GameModel Create (IGameInputManager inputManager, IAudioManager audioManager)
    {
        IPauseModel pauseModel = new PauseModel();
        ISongModel songModel = SongModelFactory.Create(inputManager, pauseModel);
        IScoreModel scoreModel = new ScoreModel(songModel);
        var model = new GameModel(songModel, pauseModel, scoreModel, inputManager, audioManager);
        return model;
    }
}