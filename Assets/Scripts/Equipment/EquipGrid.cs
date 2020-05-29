using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipGrid : ItemGrid
{
    protected EquipmentInfo equipmentInfo;
    public GameObject equipPrefab;
    protected Job job;
    protected EquipType currentEquipType;

    public int atk;
    public int def;
    public int speed;

    protected virtual void Awake()
    {
        
    }


    public virtual void Equip(ObjectInfo objectInfo)
    {
        UnEquip();
        equipmentInfo = ObjectsInfo.Instance.GetEquipmentInfo(objectInfo.id);
        GameObject equipObj = GetItem();
        
        EquipItem item =equipObj.GetComponent<EquipItem>();
        item.SetObjectInfo(equipmentInfo);
        SetItem(item);
        UpdatePropertry();
    }
    public virtual void UnEquip()
    {
        if (equipmentInfo != null)
        {
            Inventory.instance.AddObjectData(equipmentInfo,1);
            equipmentInfo = null;
            ClearGrid();
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -2)
        {
            if (item != null)
            {
                UnEquip();
                
            }
        }
    }
    public GameObject GetItem()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf == false)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                return transform.GetChild(i).gameObject;
            }
        }
        return Instantiate(equipPrefab,transform.position, transform.rotation);
    }
    public void UpdatePropertry()
    {
        if (equipmentInfo != null)
        {
            atk = equipmentInfo.atk;
            def = equipmentInfo.def;
            speed = equipmentInfo.speed;
        }
        Debug.Log(atk + " " + def + " " + speed);
    }
}
