public static class SongEditorModelFactory
{
    public static SongEditorModel Create (IEditorInputManager inputManager)
    {
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        IEditorSongPickerModel songPickerModel = new EditorSongPickerModel();
        IEditorSongModel songModel = new EditorSongModel(inputManager);
        SongEditorModel model = new SongEditorModel(songLoaderModel, songPickerModel, songModel, inputManager);
        return model;
    }
}