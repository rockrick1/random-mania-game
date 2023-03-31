public static class SongModelFactory
{
    public static SongModel Create (IGameInputManager inputManager)
    {
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        SongModel model = new SongModel(inputManager, songLoaderModel);
        return model;
    }
}