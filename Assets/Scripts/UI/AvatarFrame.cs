using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarFrame : MonoBehaviour
{
    public static AvatarFrame instance;


    private Text nameTxt;
    private Text hpTxt;
    private Text mpTxt;
    private Slider hpBar;
    private Slider mpBar;
    private RawImage faceImg;

    private void Awake()
    {
        instance = this;

        nameTxt = transform.Find("nameBg/Text").GetComponent<Text>();
        hpBar = transform.Find("HPBar").GetComponent<Slider>();
        mpBar = transform.Find("MPBar").GetComponent<Slider>();
        hpTxt = transform.Find("HPBar/valueTxt").GetComponent<Text>();
        mpTxt = transform.Find("MPBar/valueTxt").GetComponent<Text>();
        faceImg = GetComponent<RawImage>();
    }
    private void Update()
    {
        UpdateShow();
    }
    public void UpdateShow()
    {
        nameTxt.text = "Lv." + PlayerStatus.Instance.level+"  "+ PlayerStatus.Instance.playerName;
        hpBar.value = PlayerStatus.Instance.hp_remain /(float) PlayerStatus.Instance.hp;
        mpBar.value = PlayerStatus.Instance.mp_remain / (float)PlayerStatus.Instance.mp;
        hpTxt.text = PlayerStatus.Instance.hp_remain + "/" + PlayerStatus.Instance.hp;
        mpTxt.text= PlayerStatus.Instance.mp_remain+"/"+ PlayerStatus.Instance.mp;
    }
}
