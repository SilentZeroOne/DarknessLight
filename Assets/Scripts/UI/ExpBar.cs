using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public static ExpBar instance;
    private Slider slider;
    private Text text;
    private void Awake()
    {
        instance = this;
        slider = GetComponent<Slider>();
        text = transform.Find("valueTxt").GetComponent<Text>();
    }
    public void SetValue(float value)
    {
        slider.value = value;
        text.text = PlayerStatus.Instance.exp + "/" + PlayerStatus.Instance.totalExp;
    }
}
