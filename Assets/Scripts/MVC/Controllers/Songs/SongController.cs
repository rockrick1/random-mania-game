using System.Threading.Tasks;

public class SongController
{
    const string HIT_SOUND = "soft-hitnormal";
    
    public UpperSongController UpperSongController { get; }
    public ComboController ComboController { get; }
    public LowerSongController LowerSongController { get; }
    
    readonly ISongModel model;
    readonly IAudioManager audioManager;
    readonly SongView view;

    public SongController (
        SongView view,
        ISongModel model,
        IAudioManager audioManager,
        UpperSongController upperSongController,
        ComboController comboController,
        LowerSongController lowerSongController
    )
    {
        this.view = view;
        this.model = model;
        this.audioManager = audioManager;
        UpperSongController = upperSongController;
        ComboController = comboController;
        LowerSongController = lowerSongController;
    }

    public void Initialize ()
    {
        AddListeners();
        UpperSongController.Initialize();
        ComboController.Initialize();
        LowerSongController.Initialize();
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
    }

    void RemoveListeners ()
    {
        model.OnAudioStartTimeReached -= HandleAudioStartTimeReached;
        model.OnNoteHit -= HandleNoteHit;
        model.OnLongNoteHit -= HandleNoteHit;
        model.OnLongNoteReleased -= HandleNoteHit;
    }

    void HandleAudioStartTimeReached ()
    {
        view.Play();
    }

    void HandleNoteHit (Note _, HitScore __)
    {
        //TODO add dynamic hit sound check based on note settings
        audioManager.PlaySfx(HIT_SOUND);
    }
    
    public void Dispose ()
    {
        RemoveListeners();
        UpperSongController.Dispose();
        ComboController.Dispose();
        LowerSongController.Dispose();
    }
}