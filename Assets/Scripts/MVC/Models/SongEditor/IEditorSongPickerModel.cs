using System;

public interface IEditorSongPickerModel
{
    event Action<string, string> OnSongPicked;
    void PickSong (string songLabel);
    void PickSong (string songId, string songDifficultyName);
}