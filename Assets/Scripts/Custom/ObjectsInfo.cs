using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjectType
{
    Drug,
    Equipment,
    Material
}

public class ObjectsInfo : MonoBehaviour
{
    private static ObjectsInfo instance;
    public static ObjectsInfo Instance
    {
        get { return instance; }
    }

    public TextAsset objInfo;

    private Dictionary<int, ObjectInfo> objDict;
    public Dictionary<int, ObjectInfo> ObjDict
    {
        get { return objDict; }
    }
    private Dictionary<int, DrugInfo> drugDict;
    public Dictionary<int, DrugInfo> DrugDict
    {
        get { return drugDict; }
    }

    private Dictionary<int, EquipmentInfo> equipDict;
    public Dictionary<int, EquipmentInfo> EquipDict
    {
        get { return equipDict; }
    }
    private void Awake()
    {
        instance = this;
        objDict = new Dictionary<int, ObjectInfo>();
        drugDict = new Dictionary<int, DrugInfo>();
        equipDict = new Dictionary<int, EquipmentInfo>();
        ReadInfo();
        //foreach (var item in EquipDict.Values)
        //{
        //    print(item.id);
        //}
    }
    void ReadInfo()
    {
        string text = objInfo.text;
        string[] strArray=text.Split('\n');

        foreach (var item in strArray)
        {
            ObjectInfo info = new ObjectInfo();
            string[] proArray = item.Split(' ');
            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            string icon_name = proArray[2];
            string str_type = proArray[3];
            ObjectType type=ObjectType.Drug;
            switch (str_type)
            {
                case "Drug":
                    type = ObjectType.Drug;                   
                    break;
                case "Equipment":
                    type = ObjectType.Equipment;
                    break;
                case "Material":
                    type = ObjectType.Material;
                    break;
            }
            info.id = id;
            info.name = name;
            info.icon_name = icon_name;
            info.type = type;
            int price_sell;
            int price_buy;
            switch (type)
            {
                case ObjectType.Drug:
                    int hp = int.Parse(proArray[4]);
                    int mp = int.Parse(proArray[5]);
                    price_sell = int.Parse(proArray[6]);
                    price_buy = int.Parse(proArray[7]);
                    DrugInfo Druginfo = new DrugInfo(info);
                    Druginfo.hp = hp;
                    Druginfo.mp = mp;
                    Druginfo.price_sell = price_sell;
                    Druginfo.price_buy = price_buy;
                    Druginfo.spritePath = "Icon/" + Druginfo.icon_name;
                    Druginfo.count = 1;
                    drugDict.Add(Druginfo.id, Druginfo);
                    break;
                case ObjectType.Equipment:
                    int atk = int.Parse(proArray[4]);
                    int def = int.Parse(proArray[5]);
                    int speed = int.Parse(proArray[6]);
                    string str_equipType = proArray[7];
                    EquipType equipType = EquipType.Accessory;
                    switch (str_equipType)
                    {
                        case "Armor":
                            equipType = EquipType.Armor;
                            break;
                        case "Shoe":
                            equipType = EquipType.Shoe;
                            break;
                        case "Helmet":
                            equipType = EquipType.Helmet;
                            break;
                        case "Accessory":
                            equipType = EquipType.Accessory;
                            break;
                        case "Shield":
                            equipType = EquipType.Shield;
                            break;
                        case "MainWeapon":
                            equipType = EquipType.MainWeapon;
                            break;
                    }
                    string str_job = proArray[8];
                    Job job = Job.Common;
                    switch (str_job)
                    {
                        case "Swordman":
                            job = Job.Swordman;
                            break;
                        case "Magician":
                            job = Job.Magician;
                            break;
                        case "Common":
                            job = Job.Common;
                            break;
                    }
                    price_sell = int.Parse(proArray[9]);
                    price_buy = int.Parse(proArray[10]);
                    EquipmentInfo equipmentInfo = new EquipmentInfo(info);
                    equipmentInfo.atk = atk;
                    equipmentInfo.def = def;
                    equipmentInfo.speed = speed;
                    equipmentInfo.equipType = equipType;
                    equipmentInfo.job = job;
                    equipmentInfo.price_sell = price_sell;
                    equipmentInfo.price_buy = price_buy;
                    equipmentInfo.count = 1;
                    equipmentInfo.spritePath = "Icon/" + equipmentInfo.icon_name;
                    equipDict.Add(equipmentInfo.id, equipmentInfo);
                    break;
            }
            
        }
    }

    public ObjectInfo GetObjectInfo(int id)
    {
        objDict.TryGetValue(id, out ObjectInfo info);
        return info;
    }
    public DrugInfo GetDrugInfo(int id)
    {
        drugDict.TryGetValue(id, out DrugInfo info);
        return info;
    }
    public EquipmentInfo GetEquipmentInfo(int id)
    {
        equipDict.TryGetValue(id, out EquipmentInfo info);
        return info;
    }
}
