using System.Threading.Tasks;

public class SongController
{
    public UpperSongController UpperSongController { get; }
    public ComboController ComboController { get; }
    public LowerSongController LowerSongController { get; }
    
    readonly ISongModel model;
    readonly SongView view;

    public SongController (
        SongView view,
        ISongModel model,
        UpperSongController upperSongController,
        ComboController comboController,
        LowerSongController lowerSongController
    )
    {
        this.view = view;
        this.model = model;
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
    }

    void RemoveListeners ()
    {
        model.OnAudioStartTimeReached -= HandleAudioStartTimeReached;
    }

    void HandleAudioStartTimeReached ()
    {
        view.Play();
    }


    public void Dispose ()
    {
        RemoveListeners();
        UpperSongController.Dispose();
        ComboController.Dispose();
        LowerSongController.Dispose();
    }
}