public class GameModelFactory
{
    public static GameModel Create()
    {
        INoteSpawnerModel noteSpawnerModel = new NoteSpawnerModel();
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        ISongModel songModel = new SongModel(noteSpawnerModel, songLoaderModel);
        GameModel model = new GameModel(songModel);
        return model;
    }
}