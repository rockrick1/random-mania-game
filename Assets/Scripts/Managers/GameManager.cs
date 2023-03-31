using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameView view;
    [SerializeField] GameInputManager inputManager;

    public GameModel Model { get; private set; }
    public GameController Controller { get; private set; }
    public GameView View => view;

    void Start ()
    {
        Application.targetFrameRate = 60;
        Model = GameModelFactory.Create(inputManager);
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