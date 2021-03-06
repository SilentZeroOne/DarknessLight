﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : PanelBase
{
    public static SkillPanel instance;
    private List<Skill> skills = new List<Skill>();
    public GameObject skillPrefab;
    public ContentControl content;

    public SkillItem currentDrag;
    public Item currentDragItem;
    public ShortCutGrid currentGrid;
    protected override void Awake()
    {
        instance = this;
        base.Awake();

    }
    private void Start()
    {
        InitItems();
        LoadData();
        UpdateShow();
    }

    private void InitItems()
    {
        for (int i =4001 ; i <= 4006; i++)
        {
            skills.Add(SkillsInfo.Instance.SkillDict[i]);
        }
        for(int i = 5001; i <= 5006; i++)
        {
            skills.Add(SkillsInfo.Instance.SkillDict[i]);
        }
    }

    private void LoadData()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            content.SetSkillItem(LoadItems(skills[i]));
        }
    }
    public override void Show()
    {
        base.Show();
        UpdateShow();
    }

    private SkillItem LoadItems(Skill skill)
    {
        GameObject obj = Instantiate(skillPrefab);
        SkillItem skillItem = obj.GetComponent<SkillItem>();
        skillItem.SetSkillItem(skill);
        return skillItem;
    }
    private void UpdateShow()
    {
        SkillItem[] items = GetComponentsInChildren<SkillItem>();
        foreach (var item in items)
        {
            item.UpdateShow(PlayerStatus.Instance.level);
        }
    }
}
