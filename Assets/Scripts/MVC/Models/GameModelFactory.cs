public static class GameModelFactory
{
    public static GameModel Create (IInputManager inputManager)
    {
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        ISongModel songModel = new SongModel(inputManager, songLoaderModel);
        var model = new GameModel(songModel, inputManager);
        return model;
    }
}