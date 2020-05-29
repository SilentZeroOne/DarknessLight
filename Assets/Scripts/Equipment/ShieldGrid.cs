using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGrid : EquipGrid
{
    protected override void Awake()
    {
        base.Awake();
        currentEquipType = EquipType.Shield;
    }
}
