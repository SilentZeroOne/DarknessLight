using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : PanelBase
{
    public ContentControl content;
    public GameObject totalPricePanel;
    public GameObject tipsPanel;
    public Text totalPrice;
    public GameObject shopItemPrefab;

    public Action onClose;
    public Action onBuyItem;
    protected override void Awake()
    {
        base.Awake();
        totalPricePanel.SetActive(false);
    }

    protected virtual void Start()
    {
        InitItems();
        LoadData();
    }

    protected virtual void InitItems()
    {
        
    }

    protected virtual void LoadData()
    {
       
    }

}
