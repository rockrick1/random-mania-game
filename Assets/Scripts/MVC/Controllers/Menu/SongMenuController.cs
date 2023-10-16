using System;
using System.Collections.Generic;
using System.Globalization;

public class SongMenuController : IDisposable
{
    public event Action OnBackPressed;
    
    readonly SongMenuView view;
    readonly ISongMenuModel model;
    readonly MenuAnimationsController menuAnimationsController;
    
    readonly List<SongEntryController> entryControllers = new();
    
    public SongMenuController (SongMenuView view, ISongMenuModel model)
    {
        this.view = view;
        this.model = model;
        menuAnimationsController = new MenuAnimationsController(view.transform);
    }

    public void Initialize ()
    {
        AddListeners();
        model.PickFirstSong();
        menuAnimationsController.Initialize();
    }

    public void Open ()
    {
        SyncView();
        view.Open();
        menuAnimationsController.PlayOpen();
    }

    public void Close ()
    {
        menuAnimationsController.PlayClose();
    }

    void SyncView ()
    {
        view.Setup(model.GetAllSongs());
        CreateMissingInstances();
        UpdateInstances();
        SyncSongInfos();
        HighlightSelectedSong();
    }

    void CreateMissingInstances ()
    {
        int missing = model.GetAllSongs().Count - entryControllers.Count;
        for (int i = 0; i < missing; i++)
        {
            SongEntryView entryView = view.CreateEntryView();
            entryControllers.Add(new SongEntryController(entryView));
        }
    }

    void UpdateInstances ()
    {
        IReadOnlyList<ISongSettings> songs = model.GetAllSongs();
        for (int i = 0; i < entryControllers.Count; i++)
        {
            var controller = entryControllers[i];
            controller.Setup(songs[i]);
            controller.OnClick += HandleSongClicked;
        }
    }

    void SyncSongInfos ()
    {
        view.SetSelectedSongTitle(model.SelectedSongSettings.Title);
        view.SetSelectedSongArtist(model.SelectedSongSettings.Artist);
        view.SetSelectedSongDifficultyName(model.SelectedSongSettings.DifficultyName);
        view.SetSelectedSongBPM(model.SelectedSongSettings.Bpm.ToString(CultureInfo.InvariantCulture));
        view.SetSelectedSongApproachRate(model.SelectedSongSettings.ApproachRate.ToString(CultureInfo.InvariantCulture));
        view.SetSelectedSongDifficulty(model.SelectedSongSettings.Difficulty.ToString(CultureInfo.InvariantCulture));
        view.SetSelectedSongLength(model.SelectedSongSettings.LengthString);
        
        view.SetBackgroundImage(model.SelectedSongSettings.Background);
        view.SetARSliderValue(GameContext.Current.ApproachRate);
    }

    void AddListeners ()
    {
        view.OnBackPressed += HandleBackPressed;
        view.OnPlayPressed += HandlePlayPressed;
        view.OnARSliderValueChanged += HandleARSliderValueChanged;
    }

    void RemoveListeners ()
    {
        view.OnBackPressed -= HandleBackPressed;
        view.OnPlayPressed -= HandlePlayPressed;
        view.OnARSliderValueChanged -= HandleARSliderValueChanged;
    }

    void HandleSongClicked (string songId, string songDifficultyName)
    {
        if (!model.PickSong(songId, songDifficultyName))
            return;
        SyncSongInfos();
        HighlightSelectedSong();
    }

    void HighlightSelectedSong ()
    {
        foreach (SongEntryController controller in entryControllers)
        {
            if (controller.SongId == model.SelectedSongSettings.Id &&
                controller.SongDifficultyName == model.SelectedSongSettings.DifficultyName)
                controller.PlayOutlineAnimation();
            else
                controller.HideOutline();
        }
    }

    void HandleBackPressed () => OnBackPressed?.Invoke();

    void HandlePlayPressed () => model.EnterGame();

    void HandleARSliderValueChanged (float value) => model.SetAR(value);

    public void Dispose ()
    {
        model.Dispose();
        RemoveListeners();
        foreach (SongEntryController controller in entryControllers)
            controller.Dispose();
    }
}