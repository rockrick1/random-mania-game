using System;

public class EditorSongPickerModel : IEditorSongPickerModel
{
    public event Action<string, string> OnSongPicked;
    
    public void PickSong (string songLabel)
    {
        string songId = songLabel.Substring(0, songLabel.IndexOf('[') - 1);
        string songDifficultyName = songLabel.Substring(songLabel.IndexOf('[') + 1, songLabel.IndexOf(']') - songLabel.IndexOf('[') - 1);
        OnSongPicked?.Invoke(songId, songDifficultyName);
    }
    
    public void PickSong (string songId, string songDifficultyName) => OnSongPicked?.Invoke(songId, songDifficultyName);
}
