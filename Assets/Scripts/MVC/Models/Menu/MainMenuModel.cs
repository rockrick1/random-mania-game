public class MainMenuModel : IMainMenuModel
{
    public MainMenuModel ()
    {
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
    }

    void RemoveListeners ()
    {
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}