using System;

public interface IEditorSongModel : IDisposable
{
    int SelectedSignature { get; }
    float SignedBeatInterval { get; }

    void Initialize ();
    NoteCreationResult? ButtonLeftClicked (int pos, float songProgress, float height);
    int ButtonRightClicked (int pos, float songProgress, float height);
    int GetSeparatorColorByIndex (int i);
    float GetNextBeat (float time, int direction);
    void ChangeBpm (float val);
    void ChangeAr (float val);
    void ChangeDiff (float val);
    void ChangeStartingTime (float val);
    void ChangeSignature (int signature);
}