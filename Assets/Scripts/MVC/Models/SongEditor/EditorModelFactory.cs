﻿public static class EditorModelFactory
{
    public static EditorModel Create (IEditorInputManager inputManager, IAudioManager audioManager)
    {
        SongLoader songLoader = SongLoader.Instance;
        IEditorSongPickerModel songPickerModel = new EditorSongPickerModel();
        IEditorSongModel songModel = new EditorSongModel(inputManager, songLoader);
        IEditorNewSongModel newSongModel = new EditorNewSongModel(songLoader);
        IEditorHitsoundsModel hitsoundsModel = new EditorHitsoundsModel(
            songModel,
            audioManager
        );
        EditorModel model = new(
            songLoader,
            songPickerModel,
            songModel,
            newSongModel,
            hitsoundsModel,
            inputManager,
            audioManager
        );
        return model;
    }
}