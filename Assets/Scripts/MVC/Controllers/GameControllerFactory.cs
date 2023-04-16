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

        ComboController comboController = new(
            view.ComboView,
            model.ComboModel
        );

        SongController songController = new(
            view.SongView,
            model.SongModel,
            model.AudioManager,
            upperSongController,
            comboController,
            lowerSongController
        );
        GameController controller = new(songController);
        return controller;
    }
}