public static class EditorControllerFactory
{
    public static EditorController Create (EditorView view, EditorModel model)
    {
        EditorSongPickerController songPickerController =
            new EditorSongPickerController(view.EditorSongPickerView, model.SongPickerModel);
        EditorSongDetailsController songDetailsController =
            new EditorSongDetailsController(view.EditorSongDetailsView);
        EditorTopBarController topBarController = new(view.EditorTopBarView, model.InputManager);
        EditorSongController songController = new(view.EditorSongView, model.SongModel, model.SongLoaderModel);
        EditorController controller =
            new(songPickerController,
                songDetailsController,
                topBarController,
                songController,
                view,
                model,
                model.SongModel,
                model.InputManager
            );
        return controller;
    }
}