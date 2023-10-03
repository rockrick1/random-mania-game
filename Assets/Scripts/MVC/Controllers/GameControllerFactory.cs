public static class GameControllerFactory
{
    public static GameController Create (GameView view, GameModel model)
    {
        LowerSongController lowerSongController = new(
            view.SongView.LowerSongView,
            model.InputManager,
            model.SongModel
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
            model.ScoreModel,
            model.AudioManager
        );

        PauseController pauseController = new(
            view.PauseView,
            model.PauseModel,
            model.InputManager
        );

        SongController songController = new(
            view.SongView,
            pauseController,
            model.SongModel,
            model.AudioManager,
            model.SongModel.SongLoader
        );

        ResultsController resultsController = new(
            view.ResultsView,
            songController,
            model.ScoreModel
        );

        GameBackgroundController gameBackgroundController = new(
            view.GameBackgroundView,
            model.SongModel.SongLoader
        );

        SkipSongStartController skipSongStartController = new(
            view.SkipSongStartView,
            model.SongModel
        );
        
        return new GameController(
            songController,
            upperSongController,
            comboController,
            lowerSongController,
            pauseController,
            scoreController,
            resultsController,
            gameBackgroundController,
            skipSongStartController
        );
    }
}