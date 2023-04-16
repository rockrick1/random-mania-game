using UnityEngine;

public class EditorManager : MonoBehaviour
{
    [SerializeField] EditorView view;
    [SerializeField] EditorInputManager inputManager;
    [SerializeField] AudioManager audioManager;

    public EditorModel Model { get; private set; }
    public EditorController Controller { get; private set; }
    public EditorView View => view;

    void Start ()
    {
        Application.targetFrameRate = 60;
        AudioListener.volume = 0.2f;
        Model = EditorModelFactory.Create(inputManager, audioManager);
        Controller = EditorControllerFactory.Create(View, Model);
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