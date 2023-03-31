using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorSongDetailsView : MonoBehaviour
{
    public event Action OnSetOffsetClick;
    [SerializeField] TMP_InputField bpmInput;
    [SerializeField] TMP_InputField arInput;
    [SerializeField] TMP_InputField diffInput;
    [SerializeField] TMP_InputField offsetInput;
    [SerializeField] Button setOffsetButton;

    void Awake ()
    {
        setOffsetButton.onClick.AddListener(() => OnSetOffsetClick?.Invoke());
    }
}