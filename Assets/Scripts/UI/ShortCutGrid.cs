using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShortCutGrid : ItemGrid
{
    private SkillItem skillItem;
    public SkillItem SkillItem
    {
        get { return skillItem; }
    }
    public KeyCode keyCode;
    GameObject newImg;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {  
            if(skillItem!=null)       
               Debug.Log("发动魔法" + skillItem.Skill.name);
        }
    }
    public  void DragToThisGrid(SkillItem skillItem)
    {   if(this.skillItem==null)           
          SetSkill(skillItem);
        else
        {           
            ClearGrid();
            SetSkill(skillItem);
            Debug.Log(skillItem.Skill.name);
        }
    }

    public void SetSkill(SkillItem skillItem)
    {
        this.skillItem = skillItem;
        newImg = Instantiate(this.skillItem.image.gameObject);
        newImg.transform.SetParent(transform);
        newImg.transform.GetComponent<UIDrag>().enabled = false;
        newImg.transform.localPosition = Vector3.zero;
        newImg.transform.localScale = Vector3.one*0.8f;
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (SkillPanel.instance.currentDrag != null)
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
    }
}
