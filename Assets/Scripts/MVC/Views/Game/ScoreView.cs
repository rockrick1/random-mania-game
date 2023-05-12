using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI accuracyText;

    public void SetScore (string score)
    {
        scoreText.text = score;
    }
    
    public void SetAccuracy (string accuracy)
    {
        accuracyText.text = accuracy;
    }
}
