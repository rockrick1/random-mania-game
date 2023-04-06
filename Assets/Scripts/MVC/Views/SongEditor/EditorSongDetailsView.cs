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
    public event Action<double> OnOffsetChanged;
    public event Action OnSetOffsetClick;
    
    [SerializeField] TMP_InputField bpmInput;
    [SerializeField] TMP_InputField arInput;
    [SerializeField] TMP_InputField diffInput;
    [SerializeField] TMP_InputField offsetInput;
    [SerializeField] Button setOffsetButton;

    void Awake ()
    {
        bpmInput.onValueChanged.AddListener((val) => OnBPMChanged?.Invoke(float.Parse(val)));
        arInput.onValueChanged.AddListener((val) => OnARChanged?.Invoke(float.Parse(val)));
        diffInput.onValueChanged.AddListener((val) => OnDiffChanged?.Invoke(float.Parse(val)));
        offsetInput.onValueChanged.AddListener((val) => OnOffsetChanged?.Invoke(double.Parse(val)));
        setOffsetButton.onClick.AddListener(() => OnSetOffsetClick?.Invoke());
    }

    public void SetOffset (double val) => offsetInput.text = val.ToString(CultureInfo.InvariantCulture);
}