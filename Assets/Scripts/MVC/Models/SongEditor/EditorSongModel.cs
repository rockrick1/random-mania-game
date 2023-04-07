using UnityEngine;

public class EditorSongModel : IEditorSongModel
{
    const int SONG_FIELD_WIDTH = 550;
    const int VIEWPORT_WIDTH = 1920;
    
    readonly IEditorInputManager inputManager;
    
    public EditorSongModel (IEditorInputManager inputManager)
    {
        this.inputManager = inputManager;
    }

    public void ButtonClicked (int pos)
    {
        
    }

    public void Dispose ()
    {
    }
}