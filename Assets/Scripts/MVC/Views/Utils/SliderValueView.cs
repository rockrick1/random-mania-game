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
        text.text = string.IsNullOrEmpty(formatting)
            ? value.ToString(CultureInfo.InvariantCulture)
            : string.Format(formatting, value);
    }
}
