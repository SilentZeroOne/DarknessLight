using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrugShop : PanelBase
{
    public static DrugShop instance;


    public List<DrugInfo> objects = new List<DrugInfo>();
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
        instance = this;
        totalPricePanel.SetActive(false);
    }
    void Start()
    {
        InitItems();
        LoadData();
    }

    public override void Hide()
    {
        base.Hide();
        totalPrice.text = "0";
        totalPricePanel.SetActive(false);
        onClose?.Invoke();
    }
    private void InitItems()
    {
        for (int i = 1001; i <= 1003; i++)
        {
            objects.Add(ObjectsInfo.Instance.DrugDict[i]);
        }
    }

    private void LoadData()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            content.SetShopItem(LoadItems(objects[i]));
        }
    }


    private DrugShopItem LoadItems(DrugInfo drugInfo)
    {
        GameObject obj = Instantiate(shopItemPrefab);
        DrugShopItem shopItem = obj.GetComponent<DrugShopItem>();
        shopItem.SetShopItemInfo(drugInfo);
        return shopItem;
    }

    public void BuyItems()
    {

        if (int.Parse(totalPrice.text) <= PlayerStatus.Instance.coins)
        {
            PlayerStatus.Instance.coins -= int.Parse(totalPrice.text);
            foreach (var item in objects)
            {
                if(item.buyNum != 0)
                {
                    //if (!Inventory.instance.objects.Contains(item))
                    //{
                    //    Inventory.instance.objects.Add(item);
                    //    Inventory.instance.objects[Inventory.instance.objects.IndexOf(item)].count += (item.buyNum-1);
                    //}
                    //else
                    //{
                    //    Inventory.instance.objects[Inventory.instance.objects.IndexOf(item)].count += item.buyNum;
                    //}
                    Inventory.instance.AddObjectData(item, item.buyNum);
                   
                }    
            }
            totalPricePanel.SetActive(false);
        }
        else
        {
            Debug.Log("钱不够");
            tipsPanel.GetComponent<TipsPanel>().Show();
        }
        totalPrice.text = "0";
        onClose?.Invoke();
       // Inventory.instance.LoadData();
    }
}
