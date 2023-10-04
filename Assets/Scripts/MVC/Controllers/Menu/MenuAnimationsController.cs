using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuAnimationsController
{
    const string OPEN_ANIMATION = "OpenAnimation";
    const string CLOSE_ANIMATION = "CloseAnimation";
    
    readonly Transform menuView;

    List<BaseAnimation> openAnimations;
    List<BaseAnimation> closeAnimations;
    
    public MenuAnimationsController (Transform menuView)
    {
        this.menuView = menuView;
    }

    public void Initialize ()
    {
        openAnimations = menuView.transform.GetComponentsInChildren<BaseAnimation>().Where(t => t.name == OPEN_ANIMATION).ToList();
        closeAnimations = menuView.transform.GetComponentsInChildren<BaseAnimation>().Where(t => t.name == CLOSE_ANIMATION).ToList();
    }

    public void PlayOpen ()
    {
        foreach (BaseAnimation animation in openAnimations)
            animation.Play();
    }

    public void PlayClose ()
    {
        foreach (BaseAnimation animation in closeAnimations)
            animation.Play();
    }
}