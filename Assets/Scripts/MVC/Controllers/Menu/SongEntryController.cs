using System;

public class SongEntryController : IDisposable
{
    public event Action<string, string> OnClick;

    public string SongId;
    public string SongDifficultyName;
    
    readonly SongEntryView view;
    
    public SongEntryController (SongEntryView view)
    {
        this.view = view;
    }

    public void Setup (ISongSettings songSettings)
    {
        SongId = songSettings.Id;
        SongDifficultyName = songSettings.DifficultyName;
        view.Setup(songSettings);
        view.Button.OnLeftClick.RemoveAllListeners();
        view.Button.OnLeftClick.AddListener(() => OnClick?.Invoke(songSettings.Id, songSettings.DifficultyName));
        HideOutline();
    }

    public void PlayOutlineAnimation () => view.PlayOutlineAnimation();
    
    public void HideOutline () => view.HideOutline();

    public void Dispose ()
    {
        view.Button.OnLeftClick.RemoveAllListeners();
    }
}