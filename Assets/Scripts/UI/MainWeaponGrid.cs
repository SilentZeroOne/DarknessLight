using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeaponGrid : EquipGrid
{
    protected override void Awake()
    {
        base.Awake();
        currentEquipType = EquipType.MainWeapon;
    }
}
