public static class GameModelFactory
{
    public static GameModel Create (IGameInputManager inputManager, IAudioManager audioManager)
    {
        ISongModel songModel = SongModelFactory.Create(inputManager);
        IComboModel comboModel = new ComboModel(songModel);
        var model = new GameModel(songModel, comboModel, inputManager, audioManager);
        return model;
    }
}