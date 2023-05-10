using System;

public class PauseController : IDisposable
{
    public event Action OnPause;
    public event Action OnResume;

    readonly PauseView view;
    readonly IGameInputManager inputManager;
    readonly IPauseModel model;
    
    public PauseController (PauseView view, IPauseModel model, IGameInputManager inputManager)
    {
        this.view = view;
        this.model = model;
        this.inputManager = inputManager;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        inputManager.OnEscPressed += HandleEscPressed;
        view.ResumeButton.OnLeftClick.AddListener(HandleResume);
        view.RetryButton.OnLeftClick.AddListener(HandleRetry);
        view.QuitButton.OnLeftClick.AddListener(HandleQuit);
    }

    void RemoveListeners ()
    {
        inputManager.OnEscPressed -= HandleEscPressed;
        view.ResumeButton.OnLeftClick.RemoveAllListeners();
        view.RetryButton.OnLeftClick.RemoveAllListeners();
        view.QuitButton.OnLeftClick.RemoveAllListeners();
    }

    void HandleEscPressed ()
    {
        if (model.HandleEscPressed())
        {
            OnPause?.Invoke();
            view.Open();
        }
        else
            view.Close(() => OnResume?.Invoke());
    }

    void HandleResume ()
    {
        model.RaiseResume();
        view.Close(() => OnResume?.Invoke());
    }

    void HandleRetry ()
    {
        model.RaiseRetry();
        view.Close();
    }

    void HandleQuit ()
    {
        model.RaiseQuit();
        view.Close();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}