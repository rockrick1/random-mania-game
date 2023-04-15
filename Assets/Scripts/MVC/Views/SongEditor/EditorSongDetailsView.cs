using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorSongDetailsView : MonoBehaviour
{
    public event Action<float> OnBPMChanged;
    public event Action<float> OnARChanged;
    public event Action<float> OnDiffChanged;
    public event Action<float> OnStartingTimeChanged;
    public event Action<string> OnSignatureChanged;
    public event Action OnSetStartingTimeClick;
    
    [SerializeField] TMP_InputField bpmInput;
    [SerializeField] TMP_InputField arInput;
    [SerializeField] TMP_InputField diffInput;
    [SerializeField] TMP_InputField offsetInput;
    [SerializeField] Button setStartingTimeButton;
    [SerializeField] TMP_Dropdown signatureDropdown;

    void Awake ()
    {
        bpmInput.onValueChanged.AddListener((val) => OnBPMChanged?.Invoke(float.Parse(val, CultureInfo.CurrentCulture)));
        arInput.onValueChanged.AddListener((val) => OnARChanged?.Invoke(float.Parse(val, CultureInfo.CurrentCulture)));
        diffInput.onValueChanged.AddListener((val) => OnDiffChanged?.Invoke(float.Parse(val, CultureInfo.CurrentCulture)));
        offsetInput.onValueChanged.AddListener((val) => OnStartingTimeChanged?.Invoke(float.Parse(val, CultureInfo.CurrentCulture)));
        setStartingTimeButton.onClick.AddListener(() => OnSetStartingTimeClick?.Invoke());
    }

    public void SetStartingTime (double val) => offsetInput.text = val.ToString(CultureInfo.InvariantCulture);

    public void HandleSignatureChanged ()
    {
        OnSignatureChanged?.Invoke(signatureDropdown.options[signatureDropdown.value].text);
    }
}