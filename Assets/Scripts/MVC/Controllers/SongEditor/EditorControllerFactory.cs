public static class EditorControllerFactory
{
    public static EditorController Create (EditorView view, EditorModel model)
    {
        EditorSongPickerController songPickerController = new(
            view.EditorSongPickerView,
            model.SongPickerModel,
            model.SongLoaderModel
        );
        EditorSongDetailsController songDetailsController = new(
            view.EditorSongDetailsView,
            model.SongLoaderModel
        );
        EditorTopBarController topBarController = new(
            view.EditorTopBarView,
            model.InputManager
        );
        EditorSongController songController = new(
            view.EditorSongView,
            songDetailsController,
            model.SongModel,
            model.SongLoaderModel
        );
        EditorController controller = new(
            songPickerController,
            songDetailsController,
            topBarController,
            songController,
            view,
            model,
            model.SongModel,
            model.SongLoaderModel,
            model.InputManager
        );
        return controller;
    }
}