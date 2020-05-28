using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour
{
    public Image image;
    private Text skillName;
    private Text applyType;
    private Text des;
    private Text costMP;

    private Skill skill;
    private UIDrag uiDrag;

    private Action onMoveEnd;
    private float moveOriginTimer; //记时
    private float moveOriginTime = 0.2f; //时间
    private bool isMovingOrigin;
    private Vector3 originImagePos;
    private Vector3 currentLocalPos;
    private GameObject clone;
    public Skill Skill
    {
        get { return skill; }
    }

    private void Awake()
    {
        image = transform.Find("Image").GetComponent<Image>();
        skillName = transform.Find("name").GetComponent<Text>();
        applyType = transform.Find("applyType").GetComponent<Text>();
        des = transform.Find("des").GetComponent<Text>();
        costMP = transform.Find("costMP").GetComponent<Text>();
        uiDrag =image.GetComponent<UIDrag>();
        originImagePos = image.transform.localPosition;

        uiDrag.onStartDrag += OnStartDrag;
        uiDrag.onEndDrag += OnEndDrag;
    }
    private void LateUpdate()
    {
        MovingOrigin();
    }

    public void SetSkillItem(Skill skill)
    {
        this.skill = skill;
        image.sprite = Resources.Load<Sprite>(skill.spritePath);
        skillName.text = skill.name;
        applyType.text = skill.GetTypeName(skill.applyType);
        if (skill.des.Length > 7)
            des.text = skill.des.Substring(0, 7) + "\n" + skill.des.Substring(7);
        else des.text = skill.des;
        costMP.text = skill.costMp + "MP";
    }

    public void OnStartDrag()
    {      
        SkillPanel.instance.currentDrag = this;
        clone=Instantiate(image.gameObject, image.transform.position, image.transform.rotation);
        
        clone.transform.SetParent(transform);
        clone.transform.localScale = Vector3.one;
        image.transform.SetParent(SkillPanel.instance.transform);
    }

    public void OnEndDrag()
    {
        if (SkillPanel.instance.currentGrid != null)
        {
            SkillPanel.instance.currentGrid.DragToThisGrid(SkillPanel.instance.currentDrag);
        }
        MoveToOrigin(() => { });
        SkillPanel.instance.currentDrag = null;
        Destroy(clone);
        image.transform.SetParent(transform);
    }
    public void MoveToOrigin(Action onMoveEnd)
    {
        isMovingOrigin = true;
        moveOriginTimer = 0;
        currentLocalPos = image.transform.localPosition;
        this.onMoveEnd = onMoveEnd;
    }
    private void MovingOrigin()
    {
        if (isMovingOrigin)
        {
            moveOriginTimer += Time.deltaTime * (1 / moveOriginTime);
            image.transform.localPosition = Vector3.Lerp(currentLocalPos, originImagePos-new Vector3(261.63f,0,0), moveOriginTimer);
            if (moveOriginTimer >= 1)
            {
                isMovingOrigin = false;
                onMoveEnd?.Invoke();
            }
        }
    }
}
