using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    protected Image itemImg;
    protected Text number;
    private UIDrag uiDrag;
    private Canvas canvas;
    private int defaultSort;

    protected ObjectInfo objectInfo;
    public ObjectInfo ObjectInfo
    {
        get { return objectInfo; }
    }


    private Action onMoveEnd;
    private Vector3 currentLocalPos;
    private float moveOriginTimer; //记时
    private float moveOriginTime = 0.2f; //时间
    private bool isMovingOrigin;
    protected virtual void Awake()
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
        SkillPanel.instance.currentDragItem = this;
    }

    public void OnEndDrag()
    {
        if (Inventory.instance.currentGrid != null)
        {
            Inventory.instance.currentGrid.DragToThisGrid(Inventory.instance.currentDrag);
            canvas.sortingOrder = defaultSort;
        }
        else if (SkillPanel.instance.currentGrid != null)
        {
            SkillPanel.instance.currentGrid.DragToThisGrid(SkillPanel.instance.currentDragItem);
            MoveToOrigin(() => canvas.sortingOrder = defaultSort);
        }
        else
        {
            MoveToOrigin(() => canvas.sortingOrder = defaultSort);
        }
        Inventory.instance.currentDrag = null;
        SkillPanel.instance.currentDragItem = null;
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

    public virtual void SetObjectInfo(ObjectInfo objectInfo)
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
            if(transform.parent.tag==TagsManager.itemGrid)
            transform.parent.GetComponent<ItemGrid>().ClearGrid();
            if(transform.parent.tag==TagsManager.shrotcutGrid)
            transform.parent.GetComponent<ShortCutGrid>().DestroyItem();

        }
        else
        {  //更新数据
            SetObjectInfo(objectInfo);
        }

    }

    public virtual string GetItemInfo()
    {
        return objectInfo.GetItemInfo();
    }
}
