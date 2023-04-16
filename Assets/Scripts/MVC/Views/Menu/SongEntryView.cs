using System.Globalization;
using TMPro;
using UnityEngine;

public class SongEntryView : MonoBehaviour
{
    const string FORMATTING = "F1";

    [SerializeField] UIClickHandler button;
    [SerializeField] TextMeshProUGUI songName;
    [SerializeField] TextMeshProUGUI bpm;
    [SerializeField] TextMeshProUGUI ar;
    [SerializeField] TextMeshProUGUI diff;

    public UIClickHandler Button => button;
    
    public void Setup (ISongSettings song)
    {
        songName.text = song.Id;
        bpm.text = song.Bpm.ToString(FORMATTING, CultureInfo.InvariantCulture);
        ar.text = song.ApproachRate.ToString(FORMATTING, CultureInfo.InvariantCulture);
        diff.text = song.Difficulty.ToString(FORMATTING, CultureInfo.InvariantCulture);
    }
}