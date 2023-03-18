public static class GameModelFactory
{
    public static GameModel Create ()
    {
        INoteSpawnerModel noteSpawnerModel = new NoteSpawnerModel();
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        ISongModel songModel = new SongModel(noteSpawnerModel, songLoaderModel);
        var model = new GameModel(songModel);
        return model;
    }
}