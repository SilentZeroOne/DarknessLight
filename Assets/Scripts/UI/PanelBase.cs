using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelBase : MonoBehaviour
{

    protected bool isShow;
    protected DOTweenAnimation tweenAnimation;

    protected virtual void Awake()
    {
        tweenAnimation = GetComponent<DOTweenAnimation>();
        gameObject.SetActive(false);
    }
    public virtual void Show()
    {
        gameObject.SetActive(true);
        isShow = true;
        tweenAnimation.DOPlayForward();
    }

    public virtual void Hide()
    {
        tweenAnimation.DOPlayBackwards();
        isShow = false;
        Invoke("Close", 0.5f);
    }

    public void ChangeState()
    {
        if (!isShow)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    protected void Close()
    {
        gameObject.SetActive(false);
    }
}
