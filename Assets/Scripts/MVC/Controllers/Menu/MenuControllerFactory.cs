public static class MenuControllerFactory
{
    public static MenuController Create (MenuView view, MenuModel model)
    {
        MainMenuController mainMenuController = new(view.MainMenuView, model.MainMenuModel);
        SongMenuController songMenuController = new(view.SongMenuView, model.SongMenuModel);
        SettingsController settingsController = new(view.SettingsView, model.SettingsModel);
        return new MenuController(
            view,
            mainMenuController,
            songMenuController,
            settingsController,
            model
        );
    }
}