public static class EditorModelFactory
{
    public static EditorModel Create (IEditorInputManager inputManager, IAudioManager audioManager)
    {
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        IEditorSongPickerModel songPickerModel = new EditorSongPickerModel();
        IEditorSongModel songModel = new EditorSongModel(inputManager, songLoaderModel);
        EditorModel model = new EditorModel(songLoaderModel, songPickerModel, songModel, inputManager, audioManager);
        return model;
    }
}