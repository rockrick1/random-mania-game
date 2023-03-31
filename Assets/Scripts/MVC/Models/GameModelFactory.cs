public static class GameModelFactory
{
    public static GameModel Create (IGameInputManager inputManager)
    {
        ISongModel songModel = SongModelFactory.Create(inputManager);
        IComboModel comboModel = new ComboModel(songModel);
        var model = new GameModel(songModel, comboModel, inputManager);
        return model;
    }
}