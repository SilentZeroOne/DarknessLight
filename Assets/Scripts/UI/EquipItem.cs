using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItem : Item
{ 
    protected override void Awake()
    {
        itemImg = GetComponent<Image>();
        number = transform.Find("Text").GetComponent<Text>();
        number.gameObject.SetActive(false);
    }
    public override void SetObjectInfo(ObjectInfo objectInfo)
    {
        this.objectInfo = objectInfo;
        itemImg.sprite = Resources.Load<Sprite>(objectInfo.spritePath);

    }
    public override string GetItemInfo()
    {
        return objectInfo.GetEquipItemInfo();
    }
}
