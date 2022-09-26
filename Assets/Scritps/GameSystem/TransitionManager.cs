using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransitionManager : Singleton<TransitionManager>
{
    

    [Space(10)]
    public Transform TransitionPanel;
    public Transform OutsidePoint;
    public Transform CenterPoint;
    public float TransitionTime;

    private void Start()
    {
        DoEnterTransition();
    }

    public void LoadGame()
    {
        ExitTransition().Play()
                        .OnComplete(() => SceneController.Instance.LoadGameplayscene());
        
    }

    public void LoadMenu()
    {
        ExitTransition().Play()
                        .OnComplete(() => SceneController.Instance.LoadCore());

    }

    public void DoEnterTransition()
    {
        EnterTransition().Play();
    }
   
    public Tween ExitTransition()
    {
        return TransitionPanel.DOMove(CenterPoint.position, TransitionTime)
                              .SetEase(Ease.OutQuad);
    }

    public Tween EnterTransition()
    {
        return TransitionPanel.DOMove(OutsidePoint.position, TransitionTime)
                              .SetEase(Ease.OutQuad);
    }
}
