public static class SongModelFactory
{
    public static SongModel Create (IGameInputManager inputManager)
    {
        SongLoader songLoader = SongLoader.Instance;
        SongModel model = new SongModel(inputManager, songLoader);
        return model;
    }
}