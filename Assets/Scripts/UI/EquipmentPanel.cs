using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : PanelBase
{
    public static EquipmentPanel instance;
    protected override void Awake()
    {
        instance = this;
        base.Awake();
        
    }
}
