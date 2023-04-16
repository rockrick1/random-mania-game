using UnityEngine;

public class SettingsView : MonoBehaviour
{
    public void Open ()
    {
        gameObject.SetActive(true);
    }

    public void Close ()
    {
        gameObject.SetActive(false);
    }
}