using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : Shop
{
    public static WeaponShop instance;
    public List<EquipmentInfo> objects = new List<EquipmentInfo>();

    

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    protected override void InitItems()
    {
        for (int i = 2001; i < 2023; i++)
        {
            objects.Add(ObjectsInfo.Instance.EquipDict[i]);
        }
    }

    protected override void LoadData()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            content.SetShopItem(LoadItems(objects[i]));
        }
    }

    private WeaponShopItem LoadItems(EquipmentInfo equipmentInfo)
    {
        GameObject obj = Instantiate(shopItemPrefab);
        WeaponShopItem shopItem = obj.GetComponent<WeaponShopItem>();
        shopItem.SetShopItemInfo(equipmentInfo);
        return shopItem;
    }
    public void BuyItems()
    {

        if (int.Parse(totalPrice.text) <= PlayerStatus.Instance.coins)
        {
            PlayerStatus.Instance.coins -= int.Parse(totalPrice.text);
            foreach (var item in objects)
            {
                if (item.buyNum != 0)
                {
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
    }
}
