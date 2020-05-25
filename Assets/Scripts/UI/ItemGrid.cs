using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemGrid : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    protected Item item;

    public Item Item
    {
        get { return item; }
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void DragToThisGrid(Item item)
    {
        //清空以前格子
        ItemGrid lastGrid = item.transform.parent.GetComponent<ItemGrid>();
        if (this.item == null)
        {
            lastGrid.ClearGrid();
            SetItem(item);
        }
        else
        {
            //交换
            lastGrid.SetItem(this.item);
            SetItem(item);
        }
    }

    public void SetItem(Item item)
    {
        this.item = item;
        this.item.gameObject.SetActive(true);
        this.item.transform.SetParent(transform);

        //this.articleItem.transform.localPosition = Vector3.zero;
        this.item.MoveToOrigin(() => {
            //this.item.GetComponent<RectTransform>().offsetMax = new Vector2(-5, -5);
            //this.item.GetComponent<RectTransform>().offsetMin = new Vector2(5, 5);
        });
        this.item.transform.localScale = Vector3.one;
        //修改rotation
        //this.item.transform.localRotation = Quaternion.Euler(Vector3.zero);

    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (Inventory.instance.currentDrag != null)
        {
            Inventory.instance.currentGrid = this;
            //bagImg.color = new Color(1, 1, 0.23f, 0.4f);

        }
        if (item != null)
        {
            //显示当前格子物品信息
            Inventory.instance.itemInfo.Show();
            Inventory.instance.itemInfo.SetShowInfo(item.GetItemInfo());

            
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Inventory.instance.currentGrid = null;
        //bagImg.color = defaultBagImgColor;
        ////隐藏当前格子物品信息
        Inventory.instance.itemInfo.Hide();
    }

    public void ClearGrid()
    {
        item.gameObject.SetActive(false);
        item = null;
    }
}
