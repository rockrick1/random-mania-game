using System;

public interface IEditorSongModel : IDisposable
{
    int SelectedSignature { get; }
    
    void Initialize ();
    NoteCreationResult? ButtonClicked (int pos, float songProgress, float height);
    int GetSeparatorColorByIndex (int i, int signature);
    float SnapToBeat (float time);
    float GetNextBeat (float time, int direction);
}