public static class EditorModelFactory
{
    public static EditorModel Create (IEditorInputManager inputManager, IAudioManager audioManager)
    {
        ISongLoaderModel songLoaderModel = new SongLoaderModel();
        IEditorSongPickerModel songPickerModel = new EditorSongPickerModel();
        IEditorSongModel songModel = new EditorSongModel(inputManager, songLoaderModel);
        IEditorNewSongModel newSongModel = new EditorNewSongModel(songLoaderModel);
        EditorModel model = new(
            songLoaderModel,
            songPickerModel,
            songModel,
            newSongModel,
            inputManager,
            audioManager
        );
        return model;
    }
}