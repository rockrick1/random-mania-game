using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] MenuView view;
    [SerializeField] MenuInputManager inputManager;

    public MenuModel Model { get; private set; }
    public MenuController Controller { get; private set; }
    public MenuView View => view;

    AudioManager audioManager;
    
    void Start ()
    {
        audioManager = AudioManager.GetOrCreate();
        Model = MenuModelFactory.Create(inputManager, audioManager);
        Controller = MenuControllerFactory.Create(View, Model);
        Initialize();
    }

    void Initialize ()
    {
        Model.Initialize();
        Controller.Initialize();
    }

    void OnDestroy ()
    {
        Model.Dispose();
        Controller.Dispose();
    }
}