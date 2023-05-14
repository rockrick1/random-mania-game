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

    public GameController (
        SongController songController,
        UpperSongController upperSongController,
        ComboController comboController,
        LowerSongController lowerSongController,
        PauseController pauseController,
        ScoreController scoreController,
        ResultsController resultsController
    )
    {
        SongController = songController;
        UpperSongController = upperSongController;
        ComboController = comboController;
        LowerSongController = lowerSongController;
        PauseController = pauseController;
        ScoreController = scoreController;
        ResultsController = resultsController;
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
    }
}