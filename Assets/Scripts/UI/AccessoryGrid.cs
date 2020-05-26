using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryGrid : EquipGrid
{
    protected override void Awake()
    {
        base.Awake();
        currentEquipType = EquipType.Accessory;
    }
}
