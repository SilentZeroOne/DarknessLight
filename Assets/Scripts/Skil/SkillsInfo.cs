using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo : MonoBehaviour
{
    public TextAsset skillInfo;
    private static SkillsInfo instance;
    public static SkillsInfo Instance
    {
        get { return instance; }
    }

    private Dictionary<int, Skill> skillDict = new Dictionary<int, Skill>();
    public Dictionary<int, Skill> SkillDict
    {
        get { return skillDict; }
    }

    private void Awake()
    {
        instance = this;
        InitSkillInfoDict();
        //foreach (var item in skillDict.Values)
        //{
        //    print(item.id);
        //}
    }

    void InitSkillInfoDict()
    {
        string text = skillInfo.text;
        string[] skillArray = text.Split('\n');
        foreach (var item in skillArray)
        {
            string[] proArray = item.Split(',');
            Skill skill = new Skill();
            skill.id = int.Parse(proArray[0]);
            skill.name = proArray[1];
            skill.icon_name = proArray[2];
            skill.des = proArray[3];
            string applyType = proArray[4];
            switch (applyType)
            {
                case "Passive":
                    skill.applyType = ApplyType.Passive;
                    break;
                case "Buff":
                    skill.applyType = ApplyType.Buff;
                    break;
                case "SingleTarget":
                    skill.applyType = ApplyType.SingleTarget;
                    break;
                case "MultiTarget":
                    skill.applyType = ApplyType.MultiTarget;
                    break;
            }
            string applyProperty = proArray[5];
            switch (applyProperty)
            {
                case "Attack":
                    skill.applyProperty = ApplyProperty.Attack;
                    break;
                case "Def":
                    skill.applyProperty = ApplyProperty.Def;
                    break;
                case "Speed":
                    skill.applyProperty = ApplyProperty.Speed;
                    break;
                case "AttackSpeed":
                    skill.applyProperty = ApplyProperty.AttackSpeed;
                    break;
                case "HP":
                    skill.applyProperty = ApplyProperty.HP;
                    break;
                case "MP":
                    skill.applyProperty = ApplyProperty.MP;
                    break;
            }
            skill.applyValue = int.Parse(proArray[6]);
            skill.applyTime = int.Parse(proArray[7]);
            skill.costMp = int.Parse(proArray[8]);
            skill.coolDownTime = int.Parse(proArray[9]);
            string job = proArray[10];
            switch (job)
            {
                case "Swordman":
                    skill.job = Job.Swordman;
                    break;
                case "Magician":
                    skill.job = Job.Magician;
                    break;
            }
            skill.level = int.Parse(proArray[11]);
            string releaseType = proArray[12];
            switch (releaseType)
            {
                case "Self":
                    skill.releaseType = ReleaseType.Self;
                    break;
                case "Enemy":
                    skill.releaseType = ReleaseType.Enemy;
                    break;
                case "Position":
                    skill.releaseType = ReleaseType.Position;
                    break;
            }
            skill.distance = float.Parse(proArray[13]);
            skill.spritePath = "Icon/" + skill.icon_name;
            skillDict.Add(skill.id, skill);
        }
    }
    public Skill GetSkillInfoByID(int id)
    {
        skillDict.TryGetValue(id, out Skill info);
        return info;
    }
}

//作用类型
public enum ApplyType
{
    Passive,
    Buff,
    SingleTarget,
    MultiTarget
}
//作用属性
public enum ApplyProperty
{
    Attack,
    Def,
    Speed,
    AttackSpeed,
    HP,
    MP
}
//释放类型
public enum ReleaseType
{
    Self,
    Enemy,
    Position
}