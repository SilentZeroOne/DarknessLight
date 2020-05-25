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

    private void Awake()
    {
        instance = this;
        objDict = new Dictionary<int, ObjectInfo>();
        drugDict = new Dictionary<int, DrugInfo>();
        ReadInfo();
        //foreach (var item in drugDict.Values)
        //{
        //    print(item.hp);
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
            switch(type)
            {
                case ObjectType.Drug:
                    int hp = int.Parse(proArray[4]);
                    int mp = int.Parse(proArray[5]);
                    int price_sell = int.Parse(proArray[6]);
                    int price_buy = int.Parse(proArray[7]);
                    DrugInfo Druginfo = new DrugInfo(info);
                    Druginfo.hp = hp;
                    Druginfo.mp = mp;
                    Druginfo.price_sell = price_sell;
                    Druginfo.price_buy = price_buy;
                    Druginfo.spritePath = "Icon/" + Druginfo.icon_name;
                    Druginfo.count = 1;
                    drugDict.Add(Druginfo.id, Druginfo);
                    break;
            }
            
        }
    }

    public ObjectInfo GetObjectInfo(int id)
    {
        ObjectInfo info = null;
        objDict.TryGetValue(id,out info);
        return info;
    }
    public DrugInfo GetDrugInfo(int id)
    {
        DrugInfo info = null;
        drugDict.TryGetValue(id, out info);
        return info;
    }
}
