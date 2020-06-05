using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ShortCutGrid : ItemGrid
{
    private SkillItem skillItem;
    public SkillItem SkillItem
    {
        get { return skillItem; }
    }
    public KeyCode keyCode;
    GameObject newImg;

    Image mask;
    bool isCoolDown=true;

    private bool isFirst=true;

    private void Awake()
    {
        mask = transform.Find("Image").GetComponent<Image>();

        mask.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (skillItem != null)
            {
                if (isCoolDown)
                {
                    skillItem.Skill.UseSkill();
                    PlayerAttack.instance.onReleaseSuccess += CoolDown;
                }

            }
            if (item != null)
                item.ObjectInfo.UseObject();
        }
    }

     void CoolDown()
    {
        isCoolDown = false;
        StartCoroutine(ShowMask());
    }

    IEnumerator ShowMask()
    {
        mask.gameObject.SetActive(true);
        mask.DOFillAmount(0, skillItem.Skill.coolDownTime);
        yield return new WaitForSeconds(skillItem.Skill.coolDownTime);
        isCoolDown = true;
        mask.fillAmount = 1;
        mask.gameObject.SetActive(false);
    }
    public  void DragToThisGrid(SkillItem skillItem)
    {           
        if(this.skillItem==null)           
          SetSkill(skillItem);
        else
        {           
            ClearGrid();
            SetSkill(skillItem);
            Debug.Log(skillItem.Skill.name);
        }
    }
    public override  void DragToThisGrid(Item item)
    {
        //清空以前格子
        ShortCutGrid lastGrid = item.transform.parent.GetComponent<ShortCutGrid>();
        if (this.item == null)
        {
            if (lastGrid != null)
                lastGrid.ClearItemGrid();
            SetItem(item);
        }
        else
        {
            //交换
            if(lastGrid!=null)
             lastGrid.SetItem(this.item);
            SetItem(item);
        }
    }
    public override void SetItem(Item item)
    {
        this.item = item;
        if (isFirst)
        {
            newImg = Instantiate(this.item.gameObject);
            isFirst = false;
        }
        this.item.gameObject.SetActive(true);
        this.item.gameObject.transform.SetParent(transform);
        this.item.gameObject.transform.localPosition = Vector3.zero;
        this.item.gameObject.transform.localScale = Vector3.one;
    }
    public void SetSkill(SkillItem skillItem)
    {
        this.skillItem = skillItem;
        newImg = Instantiate(this.skillItem.image.gameObject);
        newImg.transform.SetParent(transform);
        newImg.transform.SetAsFirstSibling();
        newImg.transform.GetComponent<UIDrag>().enabled = false;
        newImg.transform.localPosition = Vector3.zero;
        newImg.transform.localScale = Vector3.one*0.8f;
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (SkillPanel.instance.currentDrag != null||SkillPanel.instance.currentDragItem!=null)
        {
            SkillPanel.instance.currentGrid = this;
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        SkillPanel.instance.currentGrid = null;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -2)
        {
            if (skillItem != null)
            {
                ClearGrid();
            }
        }
    }
    public override void ClearGrid()
    {
        Destroy(newImg);
        skillItem = null;
        item = null;
    }
    public  void ClearItemGrid()
    {
        item.gameObject.SetActive(false);
        item = null;
    }
    public void DestroyItem()
    {
        GameObject it = transform.Find("item(Clone)").gameObject;
        if(it!=null)
          Destroy(it);
        item = null;
    }
}
