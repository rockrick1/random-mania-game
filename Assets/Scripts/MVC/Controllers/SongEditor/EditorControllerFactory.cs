public static class EditorControllerFactory
{
    public static EditorController Create (EditorView view, EditorModel model)
    {
        EditorNewSongController newSongController = new(
            view.EditorNewSongView,
            model.NewSongModel
        );
        EditorSongPickerController songPickerController = new(
            view.EditorSongPickerView,
            newSongController,
            model.SongPickerModel,
            model.SongLoader
        );
        EditorSongDetailsController songDetailsController = new(
            view.EditorSongDetailsView,
            model.SongLoader,
            model.SongModel
        );
        EditorTopBarController topBarController = new(
            view.EditorTopBarView,
            songDetailsController,
            model.InputManager
        );
        EditorSongController songController = new(
            view.EditorSongView,
            songDetailsController,
            model.SongModel,
            model.SongLoader,
            model.AudioManager,
            model.InputManager
        );
        EditorSongSavedController songSavedController = new(
            view.EditorSongSavedView,
            model.SongModel
        );
        EditorConfirmQuitController songConfirmQuitController = new(
            view.EditorConfirmQuitView,
            model.SongModel
        );
        EditorHitsoundsController hitsoundsController = new(
            model.AudioManager,
            model.SongModel
        );
        EditorController controller = new(
            songPickerController,
            songDetailsController,
            topBarController,
            songController,
            newSongController,
            songSavedController,
            songConfirmQuitController,
            hitsoundsController,
            view,
            model,
            model.SongModel,
            model.SongLoader,
            model.InputManager,
            model.AudioManager
        );
        return controller;
    }
}