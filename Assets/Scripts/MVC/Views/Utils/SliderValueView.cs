using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string formatting;
    [SerializeField] Slider slider;

    void Awake ()
    {
        slider.onValueChanged.AddListener(UpdateText);
    }

    public void UpdateText (float value)
    {
        string valueString = value.ToString(CultureInfo.InvariantCulture);
        text.text = string.IsNullOrEmpty(formatting) ? valueString : string.Format(formatting, value);
    }
}
