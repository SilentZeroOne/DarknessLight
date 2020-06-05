using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RebornPanel : PanelBase
{
    public static RebornPanel instance;
    public Text first;
    public Text second;
    private bool isCancel;

    DOTweenAnimation firstTween;
    DOTweenAnimation secondTween;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
        firstTween = first.GetComponent<DOTweenAnimation>();
        secondTween = second.GetComponent<DOTweenAnimation>();
        first.gameObject.SetActive(false);
        second.gameObject.SetActive(false);
    }
    public override void Show()
    {
        base.Show();
        first.gameObject.SetActive(true);
        firstTween.DOPlayForward();
        second.gameObject.SetActive(false);
    }
    public override void Hide()
    {
        base.Hide();
        firstTween.DOPlayBackwards();
        secondTween.DOPlayBackwards();
    }
    public void OnCancelClick()
    {
        if (!isCancel)
        {
            isCancel = true;
            firstTween.DOPlayBackwards();
            first.gameObject.SetActive(false);
            second.gameObject.SetActive(true);
            secondTween.DOPlayForward();
        }
        else
        {
            isCancel = false;
            secondTween.DOPlayBackwards();
            second.gameObject.SetActive(false);
            first.gameObject.SetActive(true);
            firstTween.DOPlayForward();
        }

    }
    public void OnOKClick()
    {
        if (!isCancel)
        {
            GameLoad.instance.Reborn();
            Hide();
        }
    }
}
