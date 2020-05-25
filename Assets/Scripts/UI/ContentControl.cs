using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentControl : MonoBehaviour
{
    public static ContentControl instance;
    private List<DrugShopItem> shopItems=new List<DrugShopItem>();
    private void Awake()
    {
        instance = this;
    }
   

    public void SetShopItem(DrugShopItem shopItem)
    {
        shopItems.Add(shopItem);
        foreach (var item in shopItems)
        {
            item.transform.SetParent(transform);
            item.transform.localScale = Vector3.one;
            item.transform.GetComponent<RectTransform>().anchoredPosition3D=new Vector3(1,1,0);
        }
    }
}
