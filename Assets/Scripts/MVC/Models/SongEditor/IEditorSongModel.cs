using System;

public interface IEditorSongModel : IDisposable
{
    int SelectedSignature { get; }
    
    void Initialize ();
    NoteCreationResult? ButtonLeftClicked (int pos, float songProgress, float height);
    int ButtonRightClicked (int pos, float songProgress, float height);
    int GetSeparatorColorByIndex (int i, int signature);
    float GetNextBeat (float time, int direction);
    void ChangeSignature (int signature);
}