using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EditorNewSongView : MonoBehaviour
{
    public event Action<string, string> OnCreateSong;
    public event Action OnOpenFolder;
    public event Action OnEdit;
    
    [SerializeField] TMP_InputField songNameInput;
    [SerializeField] TMP_InputField artistNameInput;
    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] CanvasGroup errorBox;
    [SerializeField] UIClickHandler create;
    [SerializeField] UIClickHandler cancel;
    [SerializeField] UIClickHandler openFolder;
    [SerializeField] UIClickHandler edit;
    [SerializeField] UIClickHandler closeButton;

    [SerializeField] float shakeStrength;

    [Header("View states")]
    [SerializeField] GameObject initialState;
    [SerializeField] GameObject createdState;

    Vector3 errorOriginalPos;

    void Start ()
    {
        errorBox.alpha = 0f;
        create.OnLeftClick.AddListener(HandleCreate);
        cancel.OnLeftClick.AddListener(HandleCancel);
        closeButton.OnLeftClick.AddListener(HandleCancel);
        openFolder.OnLeftClick.AddListener(HandleOpenFolder);
        edit.OnLeftClick.AddListener(HandleEdit);
        errorOriginalPos = errorBox.transform.position;
        Close();
    }

    void HandleCreate () => OnCreateSong?.Invoke(songNameInput.text, artistNameInput.text);

    void HandleCancel ()
    {
        songNameInput.text = string.Empty;
        artistNameInput.text = string.Empty;
        Close();
    }

    void HandleOpenFolder () => OnOpenFolder?.Invoke();

    void HandleEdit () => OnEdit?.Invoke();

    public void Open ()
    {
        errorBox.alpha = 0f;
        gameObject.SetActive(true);
        SetCreationState(false);
    }

    public void Close () => gameObject.SetActive(false);

    public void SetCreationState (bool songCreated)
    {
        initialState.SetActive(!songCreated);
        createdState.SetActive(songCreated);
    }

    public void ShowError (string error)
    {
        errorBox.transform.position = errorOriginalPos;
        errorText.text = error;
        errorBox.DOFade(1f, .5f);
        errorBox.transform.DOShakePosition(.5f, shakeStrength);
    }
}
