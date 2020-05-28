using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill 
{
    public int id;
    public string name;
    public string icon_name;
    public string des;//描述
    public ApplyType applyType;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int costMp;
    public int coolDownTime;
    public Job job;
    public int level;
    public ReleaseType releaseType;
    public float distance;
    public string spritePath;

    public string GetTypeName(ApplyType applyType)
    {
        switch (applyType)
        {
            case ApplyType.Buff:
                return "增益";
            case ApplyType.MultiTarget:
                return "群体伤害";
            case ApplyType.SingleTarget:
                return "单体伤害";
            case ApplyType.Passive:
                return "恢复";
        }
        return "";
    }
}
