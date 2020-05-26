using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrugShopItem : ShopItem
{
    protected override void Start()
    {
        totalPrice = transform.root.Find("DrugShop").Find("Image").Find("Panel").Find("totalPrice").GetComponent<Text>();
    }
    public override void SetShopItemInfo(ObjectInfo objectInfo)
    {
        base.SetShopItemInfo(objectInfo);
        DrugInfo drug = ObjectsInfo.Instance.GetDrugInfo(objectInfo.id);
        image.sprite = Resources.Load<Sprite>(drug.spritePath);
        names.text = drug.name;
        price.text = drug.price_buy.ToString();
        if (drug.hp != 0)
            effect.text = "+" + drug.hp + nameof(drug.hp).ToUpper();
        else
            effect.text = "+" + drug.mp + nameof(drug.mp).ToUpper();
    }

    public override void PrintNumberToBuy()
    {
        DrugInfo durg = ObjectsInfo.Instance.GetDrugInfo(ObjectInfo.id);
        durg.buyNum += int.Parse(inputField.text);
        DrugShop.instance.onClose += ClearNum;
        totalPrice.text = (int.Parse(totalPrice.text) + durg.price_buy * int.Parse(inputField.text)).ToString();
        if (DrugShop.instance.totalPricePanel.activeSelf == false)
            DrugShop.instance.totalPricePanel.SetActive(true);
        Debug.Log(durg.id + "," + durg.buyNum);
    }


    
}

