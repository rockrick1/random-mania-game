using System;

public interface IEditorSongModel : IDisposable
{
    int SelectedSignature { get; }
    
    void Initialize ();
    void ButtonClicked (int pos);
    int GetIntervalByIndex (int i, int signature);
}