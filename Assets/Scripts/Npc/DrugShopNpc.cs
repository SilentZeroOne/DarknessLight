using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugShopNpc : NPC
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DrugShop.instance.ChangeState();
        }
    }
}
