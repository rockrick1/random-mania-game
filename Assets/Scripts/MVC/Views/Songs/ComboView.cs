using TMPro;
using UnityEngine;

public class ComboView : MonoBehaviour
{
    const string COMBO_FORMAT = "{0}x";
        
    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI text;
    
    static readonly int UpdateCombo = Animator.StringToHash("comboChanged");

    public void SetCombo (int combo)
    {
        text.text = string.Format(COMBO_FORMAT, combo);
        animator.SetTrigger(UpdateCombo);
    }
}
