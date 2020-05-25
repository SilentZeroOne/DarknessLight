using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugShopItem : ShopItem
{
    public override void SetShopItemInfo(DrugInfo drugInfo)
    {
        base.SetShopItemInfo(drugInfo);
        
        if (drugInfo.hp != 0)
            effect.text = "+" + drugInfo.hp + nameof(drugInfo.hp).ToUpper();
        else
            effect.text = "+" + drugInfo.mp + nameof(drugInfo.mp).ToUpper();
    }

    public override void PrintNumberToBuy()
    {
       // DrugInfo tempO = DrugInfo;
        DrugInfo.buyNum += int.Parse(inputField.text);
        DrugShop.instance.onClose += ClearNum;
        totalPrice.text = (int.Parse(totalPrice.text) + DrugInfo.price_buy * int.Parse(inputField.text)).ToString();
        if (DrugShop.instance.totalPricePanel.activeSelf == false)
            DrugShop.instance.totalPricePanel.SetActive(true);
        Debug.Log(DrugInfo.id + "," + DrugInfo.buyNum);
    }


    
}

