public class GameControllerFactory
{
    public static GameController Create(GameView view, GameModel model)
    {
        SongController songController = new SongController(view.SongView, model.SongModel);
        GameController controller = new GameController(songController);
        return controller;
    }
}