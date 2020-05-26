using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadgearGrid : EquipGrid
{
    protected override void Awake()
    {
        base.Awake();
        currentEquipType = EquipType.Helmet;
    }

}
