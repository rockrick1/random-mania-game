public static class GameModelFactory
{
    public static GameModel Create (IGameInputManager inputManager, IAudioManager audioManager)
    {
        ISongModel songModel = SongModelFactory.Create(inputManager);
        IPauseModel pauseModel = new PauseModel(songModel);
        IScoreModel scoreModel = new ScoreModel(songModel, SongLoader.Instance);
        
        songModel.UpdateDependencies(scoreModel);
        
        var model = new GameModel(songModel, pauseModel, scoreModel, inputManager, audioManager);
        return model;
    }
}