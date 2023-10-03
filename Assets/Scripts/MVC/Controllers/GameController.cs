using System;

public class GameController : IDisposable
{
    public SongController SongController { get; }
    public UpperSongController UpperSongController { get; }
    public ComboController ComboController { get; }
    public LowerSongController LowerSongController { get; }
    public PauseController PauseController { get; }
    public ScoreController ScoreController { get; }
    public ResultsController ResultsController { get; }
    public GameBackgroundController GameBackgroundController { get; }
    public SkipSongStartController SkipSongStartController { get; }

    public GameController (
        SongController songController,
        UpperSongController upperSongController,
        ComboController comboController,
        LowerSongController lowerSongController,
        PauseController pauseController,
        ScoreController scoreController,
        ResultsController resultsController,
        GameBackgroundController gameBackgroundController,
        SkipSongStartController skipSongStartController
    )
    {
        SongController = songController;
        UpperSongController = upperSongController;
        ComboController = comboController;
        LowerSongController = lowerSongController;
        PauseController = pauseController;
        ScoreController = scoreController;
        ResultsController = resultsController;
        GameBackgroundController = gameBackgroundController;
        SkipSongStartController = skipSongStartController;
    }

    public void Initialize ()
    {
        SongController.Initialize();
        UpperSongController.Initialize();
        ComboController.Initialize();
        LowerSongController.Initialize();
        PauseController.Initialize();
        ScoreController.Initialize();
        ResultsController.Initialize();
        GameBackgroundController.Initialize();
        SkipSongStartController.Initialize();
    }

    public void Dispose ()
    {
        SongController.Dispose();
        UpperSongController.Dispose();
        ComboController.Dispose();
        LowerSongController.Dispose();
        PauseController.Initialize();
        ScoreController.Dispose();
        ResultsController.Dispose();
        GameBackgroundController.Dispose();
        SkipSongStartController.Dispose();
    }
}