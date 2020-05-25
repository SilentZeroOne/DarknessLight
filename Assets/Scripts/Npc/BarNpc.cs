﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BarNpc : NPC
{
    public GameObject questPanel;
    private DOTweenAnimation tweener;
    public DOTweenAnimation textTweener;

    public Text text;
    private Tweener twe;

    public GameObject okBtn;
    public GameObject acceptBtn;
    public int killCount = 0;
    public int shouldKill = 10;

    private bool isInTask;
    void Start()
    {
        tweener = questPanel.GetComponent<DOTweenAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isInTask)
            {
                twe.PlayForward();
                ShowTaskProgress();
            }
            else
            {
                ShowTaskDetail();
                textTweener.DOPlayForward();
            }

            ShowQuest();
        }
    }

    void ShowQuest()
    {
        questPanel.SetActive(true);
        tweener.DOPlayForward();
       
    }

    public void CloseClick()
    {
        tweener.DOPlayBackwards();
        if (!isInTask)
            textTweener.DOPlayBackwards();
        else twe.PlayBackwards();
        Invoke("CloseQuest", 0.5f);
    }

    public void CancelClick()
    {
        isInTask = false;
        CloseClick();
        text.text = "";
        killCount = 0;
    }
    public void AcceptClick()
    {
        isInTask = true;
        ShowTaskProgress();
    }

    void ShowTaskDetail()
    {
        acceptBtn.SetActive(true);
        okBtn.SetActive(false);
    }
    void ShowTaskProgress()
    {
        text.text = "";
        twe = text.DOText("任务:\n你已杀死了" + killCount + "/<color=red>" + shouldKill +
            "</color>只狼\n\n奖励:\n<color=yellow>1000金币</color>", 1);
        twe.SetAutoKill(false);
        twe.SetEase(Ease.Linear);
        okBtn.SetActive(true);
        Invoke("SetFalseAccept", 0.1f);
    }



    public void OKClick()
    {
        if (killCount >= shouldKill)
        {
            PlayerStatus.Instance.GetCoins(1000);
            CancelClick();
        }
        else
        {
            CloseClick();
        }
    }

    void CloseQuest()
    {
        questPanel.SetActive(false);
    }
    void SetFalseAccept()
    {
        acceptBtn.SetActive(false);
    }
}
