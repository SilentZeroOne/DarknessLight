using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBar : MonoBehaviour
{

    private void Update()
    {
        #region 测试添加物品
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int num = Random.Range(1001, 1004);

            if (Inventory.instance.objects.Contains(ObjectsInfo.Instance.ObjDict[num]))
            {
                ObjectsInfo.Instance.ObjDict[num].count++;
            }
            else
            {
                Inventory.instance.objects.Add(ObjectsInfo.Instance.ObjDict[num]);
            }
            
                Inventory.instance.LoadData();
        }
        #endregion
    }
    public void OnBagBtnClick()
    {
        Inventory.instance.ChangeState();
    }

    public void OnEquipBtnClick()
    {
        EquipmentPanel.instance.ChangeState();
    }
    public void OnSettingBtnClick()
    {

    }
    public void OnSkillBtnClick()
    {

    }

    public void OnStatusBtnClick()
    {
        StatusPanel.instance.ChangeState();
    }
}
