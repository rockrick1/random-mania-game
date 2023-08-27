using System;

public class EditorSongPickerModel : IEditorSongPickerModel
{
    public event Action<string, string> OnSongPicked;
    
    public void PickSong (string songId, string songDifficultyName)
    {
        OnSongPicked?.Invoke(songId, songDifficultyName);
    }
}
