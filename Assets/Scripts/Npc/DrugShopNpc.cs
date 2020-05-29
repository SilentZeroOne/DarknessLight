using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugShopNpc : NPC
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (distanceBetweenPlayer <= distance)
                DrugShop.instance.ChangeState();
        }
    }
    protected override void Update()
    {
        base.Update();
        if (distanceBetweenPlayer > distance)
        {
            DrugShop.instance.Hide();
        }
    }
}
