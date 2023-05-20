using System;

public class EditorSongPickerModel : IEditorSongPickerModel
{
    public event Action<string> OnSongPicked;
    
    public void PickSong (string songId)
    {
        OnSongPicked?.Invoke(songId);
    }
}
