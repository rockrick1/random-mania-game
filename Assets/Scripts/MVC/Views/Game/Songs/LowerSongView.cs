using System.Collections.Generic;
using UnityEngine;

public class LowerSongView : MonoBehaviour
{
    [SerializeField] List<NoteHitterView> hitters;

    public List<NoteHitterView> Hitters => hitters;
    public float HitterYPos => transform.localPosition.y + ((RectTransform)transform).sizeDelta.y / 2;

    public void SetActiveHitter (int index)
    {
        for (var i = 0; i < hitters.Count; i++)
            hitters[i].SetHitterSelectedState(i == index);
    }

    public void PlayHitterPressed (int index) => hitters[index].SetHitterPressedState(true);

    public void PlayHitterReleased (int index) => hitters[index].SetHitterPressedState(false);

    public void PlayHitterEffect (int index) => hitters[index].PlayHitterEffect();

    public void StartLongNoteEffect (int index) => hitters[index].StartLongNoteEffect();

    public void EndLongNoteEffect (int index) => hitters[index].EndLongNoteEffect();
}