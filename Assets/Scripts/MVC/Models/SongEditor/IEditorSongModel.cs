using System;

public interface IEditorSongModel : IDisposable
{
    int SelectedSignature { get; }
    
    void Initialize ();
    NoteCreationResult? ButtonClicked (int pos, float songProgress, float height);
    int GetSeparatorColorByIndex (int i, int signature);
}