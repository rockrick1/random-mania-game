using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameView view;
    [SerializeField] GameInputManager inputManager;

    public static bool IsPaused = false;

    public GameModel Model { get; private set; }
    public GameController Controller { get; private set; }
    public GameView View => view;

    AudioManager audioManager;
    
    void Start ()
    {
        Application.targetFrameRate = 60;
        audioManager = AudioManager.GetOrCreate();
        Model = GameModelFactory.Create(inputManager, audioManager);
        Controller = GameControllerFactory.Create(View, Model);
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