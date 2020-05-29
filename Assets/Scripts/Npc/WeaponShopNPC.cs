using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopNPC : NPC
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(distanceBetweenPlayer<=distance)
               WeaponShop.instance.ChangeState();
        }
    }
    protected override void Update()
    {
        base.Update();
        if (distanceBetweenPlayer > distance)
        {
            WeaponShop.instance.Hide();
        }
    }
}
