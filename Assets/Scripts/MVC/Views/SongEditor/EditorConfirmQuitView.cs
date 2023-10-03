using System;
using DG.Tweening;
using UnityEngine;

public class EditorConfirmQuitView : MonoBehaviour
{
    public event Action OnQuitClicked;
    public event Action OnSaveAndQuitClicked;
    
    [SerializeField] UIClickHandler quitButton;
    [SerializeField] UIClickHandler saveAndQuitButton;
    [SerializeField] UIClickHandler cancelButton;
    
    [SerializeField] CanvasGroup canvasGroup;

    void Start ()
    {
        gameObject.SetActive(false);
        canvasGroup.alpha = 0;
        
        quitButton.OnLeftClick.AddListener(() => OnQuitClicked?.Invoke());
        saveAndQuitButton.OnLeftClick.AddListener(() => OnSaveAndQuitClicked?.Invoke());
        cancelButton.OnLeftClick.AddListener(Hide);
    }

    public void Show ()
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1, .2f);
    }

    void Hide ()
    {
        canvasGroup.DOFade(0, .2f).OnComplete(() => gameObject.SetActive(false));
    }
}
