public static class SongModelFactory
{
    public static SongModel Create (IGameInputManager inputManager, IPauseModel pauseModel)
    {
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        SongModel model = new SongModel(inputManager, songLoaderModel, pauseModel);
        return model;
    }
}