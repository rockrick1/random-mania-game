using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowerSongView : MonoBehaviour
{
    [SerializeField] List<NoteHitterView> hitters;

    public List<NoteHitterView> Hitters => hitters;
    public float HitterYPos => transform.localPosition.y + ((RectTransform)transform).sizeDelta.y / 2;

    public void SetActiveHitter (int index)
    {
        for (var i = 0; i < hitters.Count; i++)
            hitters[i].gameObject.SetActive(i == index);
    }

    public void PlayHitterPressed (int index) => hitters[index].PlayHitterPressed();

    public void PlayHitterReleased(int index) => hitters[index].PlayHitterReleased();

    public void PlayHitterEffect (int index) => hitters[index].PlayHitterEffect();

    public void StartLongNoteEffect(int index) => hitters[index].StartLongNoteEffect();

    public void EndLongNoteEffect(int index) => hitters[index].EndLongNoteEffect();
}