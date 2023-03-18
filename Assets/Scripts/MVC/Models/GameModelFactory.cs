public class GameModelFactory
{
    public static GameModel Create()
    {
        ISongModel songModel = new SongModel();
        GameModel model = new GameModel(songModel);
        return model;
    }
}