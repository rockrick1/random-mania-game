public static class GameModelFactory
{
    public static GameModel Create (IInputManager inputManager)
    {
        INoteSpawnerModel noteSpawnerModel = new NoteSpawnerModel();
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        ISongModel songModel = new SongModel(noteSpawnerModel, songLoaderModel);
        var model = new GameModel(songModel, inputManager);
        return model;
    }
}