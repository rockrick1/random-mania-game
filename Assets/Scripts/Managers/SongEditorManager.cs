using UnityEngine;

public class SongEditorManager : MonoBehaviour
{
    [SerializeField] SongEditorView view;
    [SerializeField] EditorInputManager inputManager;

    public SongEditorModel Model { get; private set; }
    public SongEditorController Controller { get; private set; }
    public SongEditorView View => view;

    void Start ()
    {
        Application.targetFrameRate = 60;
        Model = SongEditorModelFactory.Create(inputManager);
        Controller = SongEditorControllerFactory.Create(View, Model);
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