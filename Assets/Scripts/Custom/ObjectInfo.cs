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
        stringBuilder.Append("\n");
        stringBuilder.Append("<color=#FFF470>");
        stringBuilder.Append("名称：").Append(name).Append("\n");
        stringBuilder.Append("</color>");
        stringBuilder.Append("类型：").Append(GetTypeName(type)).Append("\n"); 
        stringBuilder.Append("数量：").Append(count).Append("\n");
        stringBuilder.Append("出售价：").Append(price_sell).Append("\n");
        return stringBuilder.ToString();
    }
    public virtual string GetEquipItemInfo()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("\n");
        stringBuilder.Append("<color=#FFF470>");
        stringBuilder.Append("名称：").Append(name).Append("\n");
        stringBuilder.Append("</color>");
        stringBuilder.Append("类型：").Append(GetTypeName(type)).Append("\n");
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
    public string GetTypeName(EquipType equipType)
    {
        switch (equipType)
        {
            case EquipType.Armor:
                return "盔甲";
            case EquipType.Accessory:
                return "饰品";
            case EquipType.Helmet:
                return "头盔";
            case EquipType.MainWeapon:
                return "主武器";
            case EquipType.Shield:
                return "盾牌";
            case EquipType.Shoe:
                return "鞋子";
        }
        return "";
    }

    public string GetTypeName(Job job)
    {
        switch (job)
        {
            case Job.Common:
                return "通用";
            case Job.Magician:
                return "魔法师";
            case Job.Swordman:
                return "剑士";
        }
        return "";
    }
    public virtual void UseObject()
    {
        count--;
        if (count == 0)
        {
            //移除物品
            Inventory.instance.RemoveObject(this);
        }
        //更新
        onDataChange?.Invoke(this);
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
    public override string GetItemInfo()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(base.GetItemInfo());
        if (hp != 0)
        {
            stringBuilder.Append("恢复HP：").Append(hp).Append("\n");
        }
        if (mp != 0)
        {
            stringBuilder.Append("恢复MP：").Append(mp).Append("\n");
        }
        return stringBuilder.ToString();
    }

    public override void UseObject()
    {
        base.UseObject();
        Debug.Log("使用药品");
    }
}
public enum EquipType
{
    Armor,
    Shoe,
    Helmet,
    Accessory,
    Shield,
    MainWeapon
}
public enum Job
{
    Swordman,
    Magician,
    Common
}

public class EquipmentInfo : ObjectInfo
{
    public int atk;
    public int def;
    public int speed;
    public EquipType equipType;
    public Job job;
    public EquipmentInfo(ObjectInfo info)
    {
        id = info.id;
        name = info.name;
        icon_name = info.icon_name;
        type = info.type;
    }
    public override string GetItemInfo()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(base.GetItemInfo());
        stringBuilder.Append("装备类型：").Append(GetTypeName(equipType)).Append("\n");
        stringBuilder.Append("装备职业：").Append(GetTypeName(job)).Append("\n");
        if (atk != 0)
        {
            stringBuilder.Append("攻击力：").Append(atk).Append("\n");
        }
        if (def != 0)
        {
            stringBuilder.Append("防御力：").Append(def).Append("\n");
        }
        if (speed != 0)
        {
            stringBuilder.Append("速度：").Append(speed).Append("\n");
        }
        return stringBuilder.ToString();
    }
    public override string GetEquipItemInfo()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(base.GetEquipItemInfo());
        stringBuilder.Append("装备类型：").Append(GetTypeName(equipType)).Append("\n");
        stringBuilder.Append("装备职业：").Append(GetTypeName(job)).Append("\n");
        if (atk != 0)
        {
            stringBuilder.Append("攻击力：").Append(atk).Append("\n");
        }
        if (def != 0)
        {
            stringBuilder.Append("防御力：").Append(def).Append("\n");
        }
        if (speed != 0)
        {
            stringBuilder.Append("速度：").Append(speed).Append("\n");
        }
        return stringBuilder.ToString();
    }

    public override void UseObject()
    {
        if (PlayerStatus.Instance.job == job||job==Job.Common)
        {
            base.UseObject();
            //Debug.Log("装备武器");
            switch (equipType)
            {
                case EquipType.Helmet:
                    EquipmentPanel.instance.headgearGrid.Equip(this);
                    break;
                case EquipType.MainWeapon:
                    EquipmentPanel.instance.weaponGrid.Equip(this);
                    break;
                case EquipType.Accessory:
                    EquipmentPanel.instance.accessoryGrid.Equip(this);
                    break;
                case EquipType.Armor:
                    EquipmentPanel.instance.armorGrid.Equip(this);
                    break;
                case EquipType.Shoe:
                    EquipmentPanel.instance.shoeGrid.Equip(this);
                    break;
                case EquipType.Shield:
                    EquipmentPanel.instance.shieldGrid.Equip(this);
                    break;
            }
          
        }
        else
        {
            Debug.Log("该职业无法装备");
            return;
        }
   
    }

}
public class MaterialInfo : ObjectInfo
{

}