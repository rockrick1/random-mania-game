public static class GameModelFactory
{
    public static GameModel Create (IGameInputManager inputManager, IAudioManager audioManager)
    {
        ISongModel songModel = SongModelFactory.Create(inputManager);
        IScoreModel scoreModel = new ScoreModel(songModel);
        var model = new GameModel(songModel, scoreModel, inputManager, audioManager);
        return model;
    }
}