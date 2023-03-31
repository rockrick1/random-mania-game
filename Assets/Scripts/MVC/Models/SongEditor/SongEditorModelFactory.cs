public static class SongEditorModelFactory
{
    public static SongEditorModel Create (IEditorInputManager inputManager)
    {
        ISongLoaderModel songModel = new SongLoaderModel();
        IEditorSongPickerModel editorSongPickerModel = new EditorSongPickerModel();
        var model = new SongEditorModel(songModel, editorSongPickerModel, inputManager);
        return model;
    }
}