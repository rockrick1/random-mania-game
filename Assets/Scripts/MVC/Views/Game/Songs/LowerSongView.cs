using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowerSongView : MonoBehaviour
{
    [SerializeField] List<Image> hitters;

    public float HitterYPos => transform.localPosition.y + ((RectTransform)transform).sizeDelta.y / 2;

    public void SetActiveHitter (int index)
    {
        for (var i = 0; i < hitters.Count; i++)
            hitters[i].gameObject.SetActive(i == index);
    }
}