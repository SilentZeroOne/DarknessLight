using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanRemainItem : MonoBehaviour
{
    List<ShortCutGrid> grids = new List<ShortCutGrid>();
    void Start()
    {
        grids.AddRange(transform.GetComponentsInChildren<ShortCutGrid>());
    }
    private void LateUpdate()
    {
        CleanItem();
    }

    private void CleanItem()
    {
        for (int i = 0; i < grids.Count; i++)
        {           
            if (grids[i].transform.childCount<2 && grids[i].item != null)
            {
                grids[i].item = null;
                Debug.Log("do");
            }
        }
    }
}
