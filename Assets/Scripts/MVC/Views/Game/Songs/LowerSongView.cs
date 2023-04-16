using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowerSongView : MonoBehaviour
{
    [SerializeField] List<Image> hitters;

    public float HitterYPos => hitters[0].transform.position.y;

    public void SetActiveHitter (int index)
    {
        for (var i = 0; i < hitters.Count; i++)
            hitters[i].gameObject.SetActive(i == index);
    }
}