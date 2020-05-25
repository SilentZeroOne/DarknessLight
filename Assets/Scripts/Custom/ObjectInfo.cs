using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ObjectInfo 
{
    public int id;
    public string name;
    public string icon_name;
    public ObjectType type;
    public int price_sell;
    public int price_buy;
    public int count;
    public Action<ObjectInfo> onDataChange;
    public string spritePath;
    public int buyNum=0;


    public void AddCount(int num)
    {
        count += num;
    }
    public virtual string GetItemInfo()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<color=#FFF470>");
        stringBuilder.Append("名称：").Append(name).Append("\n");
        stringBuilder.Append("</color>");
        stringBuilder.Append("类型：").Append(GetTypeName(type)).Append("\n");
        stringBuilder.Append("数量：").Append(count).Append("\n");
        stringBuilder.Append("出售价：").Append(price_sell);
        return stringBuilder.ToString();
    }
    public string GetTypeName(ObjectType type)
    {
        switch (type)
        {
            case ObjectType.Equipment:
                return "装备";
            case ObjectType.Material:
                return "材料";
            case ObjectType.Drug:
                return "药品";
        }
        return "";
    }



}

public class DrugInfo : ObjectInfo
{
    public int hp;
    public int mp;
    public DrugInfo(ObjectInfo info)
    {
        id = info.id;
        name = info.name;
        icon_name = info.icon_name;
        type = info.type;
    }
}

public class EquipmentInfo : ObjectInfo
{
    public int atk;
    public int def;
}
public class MaterialInfo : ObjectInfo
{

}