public static class GameControllerFactory
{
    public static GameController Create (GameView view, GameModel model)
    {
        var lowerSongController = new LowerSongController(
            view.SongView.LowerSongView,
            model.InputManager
        );
        var songController = new SongController(view.SongView, model.SongModel, lowerSongController);
        var controller = new GameController(songController);
        return controller;
    }
}