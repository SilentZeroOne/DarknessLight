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
            int num = Random.Range(2001, 2023);

            Inventory.instance.AddObjectData(ObjectsInfo.Instance.EquipDict[num], 1);
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
        SkillPanel.instance.ChangeState();
    }

    public void OnStatusBtnClick()
    {
        StatusPanel.instance.ChangeState();
    }
}
