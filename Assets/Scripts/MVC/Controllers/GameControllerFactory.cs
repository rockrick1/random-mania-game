public static class GameControllerFactory
{
    public static GameController Create (GameView view, GameModel model)
    {
        LowerSongController lowerSongController = new(
            view.SongView.LowerSongView,
            model.InputManager
        );

        UpperSongController upperSongController = new(
            view.SongView.UpperSongView,
            lowerSongController,
            model.SongModel
        );
        
        var songController = new SongController(view.SongView, model.SongModel, upperSongController, lowerSongController);
        var controller = new GameController(songController);
        return controller;
    }
}