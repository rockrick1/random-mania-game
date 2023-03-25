public static class SongModelFactory
{
    public static SongModel Create (IInputManager inputManager)
    {
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        SongModel model = new SongModel(inputManager, songLoaderModel);
        return model;
    }
}