using System.Threading.Tasks;

public class SongController
{
    const string HIT_SOUND = "soft-hitnormal";
    
    public UpperSongController UpperSongController { get; }
    public ComboController ComboController { get; }
    public LowerSongController LowerSongController { get; }
    public PauseController PauseController { get; }
    public ScoreController ScoreController { get; }
    
    readonly ISongModel model;
    readonly IAudioManager audioManager;
    readonly SongView view;

    public SongController (
        SongView view,
        ISongModel model,
        IAudioManager audioManager,
        UpperSongController upperSongController,
        ComboController comboController,
        LowerSongController lowerSongController,
        PauseController pauseController,
        ScoreController scoreController
    )
    {
        this.view = view;
        this.model = model;
        this.audioManager = audioManager;
        UpperSongController = upperSongController;
        ComboController = comboController;
        LowerSongController = lowerSongController;
        PauseController = pauseController;
        ScoreController = scoreController;
    }

    public void Initialize ()
    {
        AddListeners();
        UpperSongController.Initialize();
        ComboController.Initialize();
        LowerSongController.Initialize();
        PauseController.Initialize();
        ScoreController.Initialize();
        StartSong().Start();
    }

    async Task StartSong ()
    {
        await Task.Delay(500);
        view.SetClip(model.CurrentSongAudio);
        model.Play();
    }

    void AddListeners ()
    {
        model.OnAudioStartTimeReached += HandleAudioStartTimeReached;
        model.OnNoteHit += HandleNoteHit;
        model.OnLongNoteHit += HandleNoteHit;
        model.OnLongNoteReleased += HandleNoteHit;
        PauseController.OnPause += HandlePause;
        PauseController.OnResume += HandleResume;
        PauseController.OnRetry += HandleRetry;
        PauseController.OnQuit += HandleQuit;
    }

    void RemoveListeners ()
    {
        model.OnAudioStartTimeReached -= HandleAudioStartTimeReached;
        model.OnNoteHit -= HandleNoteHit;
        model.OnLongNoteHit -= HandleNoteHit;
        model.OnLongNoteReleased -= HandleNoteHit;
        PauseController.OnPause -= HandlePause;
        PauseController.OnResume -= HandleResume;
        PauseController.OnRetry -= HandleRetry;
        PauseController.OnQuit -= HandleQuit;
    }


    void HandleAudioStartTimeReached () => view.Play();

    void HandleNoteHit (Note _, HitScore __)
    {
        //TODO add dynamic hit sound check based on note settings
        audioManager.PlaySfx(HIT_SOUND);
    }

    void HandlePause ()
    {
        GameManager.IsPaused = true;
        view.Pause();
    }

    void HandleResume ()
    {
        GameManager.IsPaused = false;
        view.Play();
    }
    void HandleRetry ()
    {
        GameManager.IsPaused = false;
    }

    void HandleQuit ()
    {
        GameManager.IsPaused = false;
    }

    public void Dispose ()
    {
        RemoveListeners();
        UpperSongController.Dispose();
        ComboController.Dispose();
        LowerSongController.Dispose();
        PauseController.Initialize();
        ScoreController.Dispose();
    }
}