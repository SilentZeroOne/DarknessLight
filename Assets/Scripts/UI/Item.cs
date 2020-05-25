using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Image itemImg;
    private Text number;
    private UIDrag uiDrag;
    private Canvas canvas;
    private int defaultSort;

    private ObjectInfo objectInfo;
    public ObjectInfo ObjectInfo
    {
        get { return objectInfo; }
    }


    private Action onMoveEnd;
    private Vector3 currentLocalPos;
    private float moveOriginTimer; //记时
    private float moveOriginTime = 0.2f; //时间
    private bool isMovingOrigin;
    private void Awake()
    {
        itemImg = GetComponent<Image>();
        number = transform.Find("Text").GetComponent<Text>();
        uiDrag = GetComponent<UIDrag>();
        canvas = GetComponent<Canvas>();
        defaultSort = canvas.sortingOrder;

        uiDrag.onStartDrag += OnStartDrag;
        uiDrag.onEndDrag += OnEndDrag;
        uiDrag.onDrag += OnDrag;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        MovingOrigin();
    }
    public void OnStartDrag()
    {
        canvas.sortingOrder = defaultSort + 1;
        Inventory.instance.currentDrag = this;
    }

    public void OnEndDrag()
    {
        if (Inventory.instance.currentGrid != null)
        {
            Inventory.instance.currentGrid.DragToThisGrid(Inventory.instance.currentDrag);
            canvas.sortingOrder = defaultSort;
        }
        else
        {
            MoveToOrigin(() => canvas.sortingOrder = defaultSort);
        }
        Inventory.instance.currentDrag = null;
    }
    public void OnDrag()
    {
       
    }
    public void MoveToOrigin(Action onMoveEnd)
    {
        isMovingOrigin = true;
        moveOriginTimer = 0;
        currentLocalPos = transform.localPosition;
        this.onMoveEnd = onMoveEnd;
    }
    private void MovingOrigin()
    {
        if (isMovingOrigin)
        {
            moveOriginTimer += Time.deltaTime * (1 / moveOriginTime);
            transform.localPosition = Vector3.Lerp(currentLocalPos, Vector3.zero, moveOriginTimer);
            if (moveOriginTimer >= 1)
            {
                isMovingOrigin = false;
                onMoveEnd?.Invoke();
            }
        }
    }

    public void SetObjectInfo(ObjectInfo objectInfo)
    {
        this.objectInfo = objectInfo;

        //绑定事件
        this.objectInfo.onDataChange = OnDataChange;
        itemImg.sprite = Resources.Load<Sprite>(objectInfo.spritePath);
        number.text = objectInfo.count.ToString();

    }
    public void OnDataChange(ObjectInfo objectInfo)
    {
        if (objectInfo.count == 0)
        {
            //清空格子
            transform.parent.GetComponent<ItemGrid>().ClearGrid();

        }
        else
        {  //更新数据
            SetObjectInfo(objectInfo);
        }

    }

    public string GetItemInfo()
    {
        return objectInfo.GetItemInfo();
    }
}
