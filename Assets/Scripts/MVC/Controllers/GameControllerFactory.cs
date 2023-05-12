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

        ScoreController scoreController = new(
            view.ScoreView,
            model.ScoreModel
        );

        ComboController comboController = new(
            view.ComboView,
            model.ScoreModel
        );

        PauseController pauseController = new(
            view.PauseView,
            model.PauseModel,
            model.InputManager
        );

        SongController songController = new(
            view.SongView,
            model.SongModel,
            model.AudioManager,
            upperSongController,
            comboController,
            lowerSongController,
            pauseController,
            scoreController
        );
        GameController controller = new(songController);
        return controller;
    }
}