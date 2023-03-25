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
        UpperSongController.Initialize();
        ComboController.Initialize();
        LowerSongController.Initialize();
        view.SetClip(model.CurrentSongAudio);
        view.Play();
        model.Play();
        AddListeners();
    }

    void AddListeners ()
    {
    }

    void RemoveListeners ()
    {
    }


    public void Dispose ()
    {
        RemoveListeners();
        UpperSongController.Dispose();
        ComboController.Dispose();
        LowerSongController.Dispose();
    }
}