using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorPanelControl : MonoBehaviour
{

    public GameObject panel;
    public static ErrorPanelControl instance;
    private void Awake()
    {
        instance = this;
    }
    public void OnClickBtn()
    {
        Invoke("OnOkBtnClick", 0.5f);
    }

    private void OnOkBtnClick()
    {
        panel.SetActive(false);
    }
    public  void ShowPanel()
    {
         instance.panel.SetActive(true);
    }
}
