using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeGrid : EquipGrid
{
    protected override void Awake()
    {
        base.Awake();
        currentEquipType = EquipType.Shoe;
    }
}
