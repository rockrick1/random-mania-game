using UnityEngine;
using UnityEngine.UI;

public class GameBackgroundView : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    
    public void SetBackground(Sprite sprite) => backgroundImage.sprite = sprite;
}