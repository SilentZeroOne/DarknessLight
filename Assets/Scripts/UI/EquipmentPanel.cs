using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : PanelBase
{
    public static EquipmentPanel instance;
    public HeadgearGrid headgearGrid;
    public MainWeaponGrid weaponGrid;
    public AccessoryGrid accessoryGrid;
    public ShieldGrid shieldGrid;
    public ArmorGrid armorGrid;
    public ShoeGrid shoeGrid;
    protected override void Awake()
    {
        instance = this;
        base.Awake();
        
    }
}
