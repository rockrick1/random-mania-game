public static class SongEditorControllerFactory
{
    public static SongEditorController Create (SongEditorView view, SongEditorModel model)
    {
        EditorSongPickerController songPickerController =
            new EditorSongPickerController(view.EditorSongPickerView, model.EditorSongPickerModel);
        EditorSongDetailsController songDetailsController =
            new EditorSongDetailsController(view.EditorSongDetailsView);
        EditorTopBarController topBarController = new(view.EditorTopBarView, model.InputManager);
        SongEditorController controller =
            new(songPickerController, songDetailsController, topBarController, view, model, model.InputManager);
        return controller;
    }
}