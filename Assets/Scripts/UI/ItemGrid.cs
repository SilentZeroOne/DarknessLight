using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemGrid : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public Item item;

    public Item Item
    {
        get { return item; }
    }


    public virtual void DragToThisGrid(Item item)
    {
        //清空以前格子
        ItemGrid lastGrid = item.transform.parent.GetComponent<ItemGrid>();
        //Debug.Log(lastGrid.name);
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

    public virtual void SetItem(Item item)
    {
        this.item = item;
        this.item.gameObject.SetActive(true);
        this.item.transform.SetParent(transform);

        //this.articleItem.transform.localPosition = Vector3.zero;
        this.item.MoveToOrigin(() => {
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
        ////隐藏当前格子物品信息
        Inventory.instance.itemInfo.Hide();
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -2)
        {
            if (item != null)
            {
                item.ObjectInfo.UseObject();
            }
        }
    }
    public virtual void ClearGrid()
    {
        item.gameObject.SetActive(false);
        item = null;
       
    }
}
