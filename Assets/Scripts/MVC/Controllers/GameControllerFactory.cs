public static class GameControllerFactory
{
    public static GameController Create (GameView view, GameModel model)
    {
        UpperSongController upperSongController = new(
            view.SongView.UpperSongView,
            model.SongModel
        );
        
        LowerSongController lowerSongController = new(
            view.SongView.LowerSongView,
            model.InputManager
        );
        var songController = new SongController(view.SongView, model.SongModel, upperSongController, lowerSongController);
        var controller = new GameController(songController);
        return controller;
    }
}