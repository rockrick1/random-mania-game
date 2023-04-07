public static class SongEditorModelFactory
{
    public static EditorModel Create (IEditorInputManager inputManager)
    {
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        IEditorSongPickerModel songPickerModel = new EditorSongPickerModel();
        IEditorSongModel songModel = new EditorSongModel(inputManager, songLoaderModel);
        EditorModel model = new EditorModel(songLoaderModel, songPickerModel, songModel, inputManager);
        return model;
    }
}