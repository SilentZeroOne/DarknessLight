using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanInventoryRemainItem : MonoBehaviour
{
    List<ItemGrid> grids = new List<ItemGrid>();
    void Start()
    {
        grids.AddRange(transform.GetComponentsInChildren<ItemGrid>());
    }
    private void LateUpdate()
    {
        CleanItem();
    }

    private void CleanItem()
    {
        for (int i = 0; i < grids.Count; i++)
        {
            if (grids[i].transform.childCount < 1 && grids[i].item != null)
            {
                grids[i].item = null;
                //Debug.Log("do inventory");
            }
        }
    }
}
