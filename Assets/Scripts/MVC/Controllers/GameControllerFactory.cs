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
            model.SongModel,
            model.ScoreModel
        );

        ScoreController scoreController = new(
            view.ScoreView,
            model.ScoreModel,
            model.SongModel
        );

        ComboController comboController = new(
            view.ComboView,
            model.ScoreModel,
            model.SongModel,
            model.AudioManager
        );

        PauseController pauseController = new(
            view.PauseView,
            model.PauseModel,
            model.InputManager
        );

        ResultsController resultsController = new(
            view.ResultsView,
            model.ScoreModel,
            model.SongModel
        );

        GameBackgroundController gameBackgroundController = new(
            view.GameBackgroundView,
            model.SongModel.SongLoader
        );

        SkipSongStartController skipSongStartController = new(
            view.SkipSongStartView,
            model.SongModel
        );

        SongController songController = new(
            view.SongView,
            pauseController,
            resultsController,
            model.SongModel,
            model.AudioManager,
            model.SongModel.SongLoader
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