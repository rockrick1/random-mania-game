using System;
using System.Globalization;

public class EditorSongDetailsController : IDisposable
{
    public event Action<float, float, float, float> OnApplyClicked;
    public event Action<int> OnSignatureChanged;
    public event Action<float> OnPlaybackSpeedChanged;
    public event Action<bool> OnShowWaveClicked;

    readonly EditorSongDetailsView view;
    readonly ISongLoaderModel songLoaderModel;
    
    public EditorSongDetailsController (EditorSongDetailsView view, ISongLoaderModel songLoaderModel)
    {
        this.view = view;
        this.songLoaderModel = songLoaderModel;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        view.OnApplyClicked += HandleApplyClicked;
        view.OnSignatureChanged += HandleSignatureChanged;
        view.OnPlaybackSpeedChanged += HandlePlaybackSpeedChanged;
        view.OnShowWaveClicked += HandleShowWaveClicked;
        songLoaderModel.OnSongLoaded += HandleSongLoaded;
    }

    void RemoveListeners ()
    {
        view.OnApplyClicked -= HandleApplyClicked;
        view.OnSignatureChanged -= HandleSignatureChanged;
        view.OnPlaybackSpeedChanged -= HandlePlaybackSpeedChanged;
        view.OnShowWaveClicked -= HandleShowWaveClicked;
        songLoaderModel.OnSongLoaded -= HandleSongLoaded;
    }

    void HandleApplyClicked ()
    {
        float bpm = float.Parse(view.BpmValue, CultureInfo.CurrentCulture);
        float ar = float.Parse(view.ArValue, CultureInfo.CurrentCulture);
        float diff = float.Parse(view.DiffValue, CultureInfo.CurrentCulture);
        float startingTime = float.Parse(view.StartingTimeValue, CultureInfo.CurrentCulture);
        OnApplyClicked?.Invoke(bpm, ar, diff, startingTime);
    }

    void HandleSignatureChanged (string signature)
    {
        OnSignatureChanged?.Invoke(int.Parse(signature.Substring(signature.IndexOf('/') + 1)));
    }

    void HandlePlaybackSpeedChanged (string speed)
    {
        OnPlaybackSpeedChanged?.Invoke(float.Parse(speed.Replace("x", ""), CultureInfo.InvariantCulture));
    }

    void HandleShowWaveClicked (bool active)
    {
        OnShowWaveClicked?.Invoke(active);
    }
    
    void HandleSongLoaded ()
    {
        view.SetBPM(songLoaderModel.Settings.Bpm);
        view.SetAR(songLoaderModel.Settings.ApproachRate);
        view.SetDiff(songLoaderModel.Settings.Difficulty);
        view.SetStartingTime(songLoaderModel.Settings.StartingTime);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}