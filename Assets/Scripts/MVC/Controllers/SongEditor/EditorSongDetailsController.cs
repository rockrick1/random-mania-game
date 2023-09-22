using System;
using System.Globalization;

public class EditorSongDetailsController : IDisposable
{
    public event Action<float, float, float, float> OnApplyClicked;
    public event Action<int> OnSignatureChanged;
    public event Action<float> OnPlaybackSpeedChanged;
    public event Action<bool> OnShowWaveClicked;

    readonly EditorSongDetailsView view;
    readonly SongLoader songLoader;
    readonly IEditorSongModel editorSongModel;

    public EditorSongDetailsController (
        EditorSongDetailsView view,
        SongLoader songLoader,
        IEditorSongModel editorSongModel
    )
    {
        this.view = view;
        this.songLoader = songLoader;
        this.editorSongModel = editorSongModel;
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
        editorSongModel.OnSongRefreshed += HandleSongRefreshed;
    }

    void RemoveListeners ()
    {
        view.OnApplyClicked -= HandleApplyClicked;
        view.OnSignatureChanged -= HandleSignatureChanged;
        view.OnPlaybackSpeedChanged -= HandlePlaybackSpeedChanged;
        view.OnShowWaveClicked -= HandleShowWaveClicked;
        editorSongModel.OnSongRefreshed -= HandleSongRefreshed;
    }

    void HandleApplyClicked ()
    {
        float bpm = float.Parse(view.BpmValue, CultureInfo.CurrentCulture);
        float ar = float.Parse(view.ArValue, CultureInfo.CurrentCulture);
        float diff = float.Parse(view.DiffValue, CultureInfo.CurrentCulture);
        float startingTime = float.Parse(view.StartingTimeValue, CultureInfo.CurrentCulture);
        OnApplyClicked?.Invoke(bpm, ar, diff, startingTime);
    }

    void HandleSignatureChanged (string signature) =>
        OnSignatureChanged?.Invoke(int.Parse(signature.Substring(signature.IndexOf('/') + 1)));

    void HandlePlaybackSpeedChanged (string speed) =>
        OnPlaybackSpeedChanged?.Invoke(float.Parse(speed.Replace("x", ""), CultureInfo.InvariantCulture));

    void HandleShowWaveClicked (bool active) => OnShowWaveClicked?.Invoke(active);

    void HandleSongRefreshed ()
    {
        ISongSettings settings = songLoader.GetSelectedSongSettings();
        view.SetBPM(settings.Bpm);
        view.SetAR(settings.ApproachRate);
        view.SetDiff(settings.Difficulty);
        view.SetStartingTime(settings.StartingTime);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}