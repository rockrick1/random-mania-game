using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] MenuView view;
    [SerializeField] MenuInputManager inputManager;
    [SerializeField] AudioManager audioManager;

    public MenuModel Model { get; private set; }
    public MenuController Controller { get; private set; }
    public MenuView View => view;

    void Start ()
    {
        Model = MenuModelFactory.Create(inputManager, audioManager);
        Controller = MenuControllerFactory.Create(View, Model);
        Initialize();
    }

    void Initialize ()
    {
        audioManager.Initialize();
        Model.Initialize();
        Controller.Initialize();
    }

    void OnDestroy ()
    {
        Model.Dispose();
        Controller.Dispose();
    }
}