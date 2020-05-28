using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentControl : MonoBehaviour
{
    public static ContentControl instance;
    private List<DrugShopItem> shopItems=new List<DrugShopItem>();
    private List<SkillItem> skillItems = new List<SkillItem>();
    
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
    public void SetSkillItem(SkillItem skillItem)
    {
        skillItems.Add(skillItem);
        foreach (var item in skillItems)
        {
            if (item.Skill.job != PlayerStatus.Instance.job)
            {
                Destroy(item.gameObject);
                continue;
            }
            item.transform.SetParent(transform);
            item.transform.localScale = Vector3.one;
            item.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(1, 1, 0);
        }
    }
}
