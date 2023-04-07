using System;

public interface IEditorSongModel : IDisposable
{
    void Initialize ();
    void ButtonClicked (int pos);
}