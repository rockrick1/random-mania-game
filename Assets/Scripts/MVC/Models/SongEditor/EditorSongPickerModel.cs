using System;

public class EditorSongPickerModel : IEditorSongPickerModel
{
    public event Action<string> OnSongPicked;
    
    public void PickSong (string song)
    {
        OnSongPicked?.Invoke(song);
    }
}
