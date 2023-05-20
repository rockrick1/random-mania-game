using System;

public interface IEditorSongPickerModel
{
    event Action<string> OnSongPicked;
    void PickSong (string songId);
}