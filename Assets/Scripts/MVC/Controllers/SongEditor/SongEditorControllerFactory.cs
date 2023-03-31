public static class SongEditorControllerFactory
{
    public static SongEditorController Create (SongEditorView view, SongEditorModel model)
    {
        EditorSongPickerController songPickerController =
            new EditorSongPickerController(view.EditorSongPickerView, model.EditorSongPickerModel);
        EditorSongDetailsController songDetailsController =
            new EditorSongDetailsController(view.EditorSongDetailsView);
        SongEditorController controller =
            new(songPickerController, songDetailsController, view, model, model.InputManager);
        return controller;
    }
}