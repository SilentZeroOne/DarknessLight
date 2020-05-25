using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsPanel : PanelBase
{


    public override void Show()
    {
        base.Show();
        Invoke("Hide", 0.5f);
    }

}
