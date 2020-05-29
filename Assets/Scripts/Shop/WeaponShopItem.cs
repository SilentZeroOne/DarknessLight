using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopItem : ShopItem
{
    private Text jobName;
    protected override void Awake()
    {
        base.Awake();
        jobName = transform.Find("jobName").GetComponent<Text>();
    }
    protected override void Start()
    {
        totalPrice = transform.root.Find("WeaponShop/Image/Panel/totalPrice").GetComponent<Text>();

    }
    public override void SetShopItemInfo(ObjectInfo objectInfo)
    {
        base.SetShopItemInfo(objectInfo);
        EquipmentInfo equip = ObjectsInfo.Instance.GetEquipmentInfo(objectInfo.id);
        image.sprite = Resources.Load<Sprite>(equip.spritePath);
        names.text = equip.name;
        price.text = equip.price_buy.ToString();
        if (equip.atk != 0)
            effect.text = "+" + equip.atk + nameof(equip.atk).ToUpper();
        else if(equip.speed!=0)
            effect.text = "+" + equip.speed + nameof(equip.speed).ToUpper();
        else
            effect.text = "+" + equip.def + nameof(equip.def).ToUpper();
        jobName.text = equip.GetTypeName(equip.job);
    }

    public override void PrintNumberToBuy()
    {
        EquipmentInfo equip = ObjectsInfo.Instance.GetEquipmentInfo(ObjectInfo.id);
        equip.buyNum += int.Parse(inputField.text);
        WeaponShop.instance.onClose += ClearNum;
        totalPrice.text = (int.Parse(totalPrice.text) + equip.price_buy * int.Parse(inputField.text)).ToString();
        if (WeaponShop.instance.totalPricePanel.activeSelf == false)
            WeaponShop.instance.totalPricePanel.SetActive(true);
        Debug.Log(equip.id + "," + equip.buyNum);
    }
}
