using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Inventory : PanelBase
{
    public static Inventory instance;
    //private DOTweenAnimation tweenAnimation;

    public ItemGrid currentGrid;
    public Item currentDrag;
    public ItemInfo itemInfo;
    public Text goldText;

    public List<ItemGrid> grids=new List<ItemGrid>();
    public List<ObjectInfo> objects = new List<ObjectInfo>();
    public List<GameObject> items = new List<GameObject>();
    public GameObject itemPrefab;

   // private bool isShow=false;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    //{

       
        //InitData();
        //tweenAnimation = GetComponent<DOTweenAnimation>();      
        //gameObject.SetActive(false);
        
   // }
    void Start()
    {
       // InitData();     
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        //#region 测试添加物品
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    int num = Random.Range(1001, 1004);

        //    if (objects.Contains(ObjectsInfo.Instance.ObjDict[num]))
        //    {
        //        ObjectsInfo.Instance.ObjDict[num].count++;
        //    }
        //    else
        //    {
        //        objects.Add(ObjectsInfo.Instance.ObjDict[num]);
        //    }
        //    LoadData();
        //}
        //#endregion

    }
    public void InitData()
    {
        objects.Add(ObjectsInfo.Instance.EquipDict[2003]);
        objects.Add(ObjectsInfo.Instance.EquipDict[2022]);
    }

    public void LoadData()
    {
        HideAllItem();
        for (int i = 0; i < objects.Count; i++)
        {
            GetGrid().SetItem(LoadItem(objects[i]));
        }
    }

    public ItemGrid GetGrid()//获得一个空格子
    {
        for (int i = 0; i < grids.Count; i++)
        {
            if (grids[i].Item == null)
            {
                return grids[i];
            }
        }
        return null;
    }
    public Item LoadItem(ObjectInfo objectInfo)
    {
        GameObject obj = GetItem();
        Item item = obj.GetComponent<Item>();
        item.SetObjectInfo(objectInfo);
        return item;
    }

    public GameObject GetItem()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].activeSelf == false)
            {
                items[i].SetActive(true);
                return items[i];
            }
        }
        return Instantiate(itemPrefab);
    }

    public void RemoveObject(ObjectInfo objectInfo)
    {
        objects.Remove(objectInfo);
    }
    public void AddObjectData(ObjectInfo objectInfo,int num)
    {
        
        if (objects.Contains(objectInfo))
        {
            objectInfo.count += num;
            for (int i = 0; i < grids.Count; i++)
            {
                if (grids[i].Item != null)
                {
                    if (grids[i].Item.ObjectInfo ==objectInfo)
                    {
                        grids[i].Item.SetObjectInfo(objectInfo);
                        break;
                    }

                }
            }
            
        }
        else
        {
            objects.Add(objectInfo);
            objectInfo.count = 0;
            objectInfo.count += num;
            Item item = GetItem().GetComponent<Item>();
            item.SetObjectInfo(objectInfo);
            GetGrid().SetItem(item);
        }

    }

    public void HideAllItem()
    {
        for (int i = 0; i < grids.Count; i++)
        {
            if (grids[i].Item != null)
            {
                grids[i].ClearGrid();
            }
        }
    }
    //void ShowInventory()
    //{
    //    gameObject.SetActive(true);
    //    isShow = true;
    //    goldText.text = PlayerStatus.Instance.coins.ToString();
    //    tweenAnimation.DOPlayForward();
    //}
    public override void Show()
    {
        base.Show();
        goldText.text = PlayerStatus.Instance.coins.ToString();
    }
    //void CloseInventory()
    //{
    //    tweenAnimation.DOPlayBackwards();
    //    isShow = false;
    //    Invoke("Close", 0.5f);
    //}

    //public void ChangeState()
    //{
    //    if (!isShow)
    //    {
    //        ShowInventory();
    //    }
    //    else
    //    {
    //        CloseInventory();
    //    }
    //}
    //void Close()
    //{
    //    gameObject.SetActive(false);
    //}
}
