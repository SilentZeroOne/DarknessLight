using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorGrid : EquipGrid
{
    protected override void Awake()
    {
        base.Awake();
        currentEquipType = EquipType.Armor;
    }
}
