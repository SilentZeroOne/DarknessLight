using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : PanelBase
{
    public static StatusPanel instance;

    private Text atk;
    private Text def;
    private Text speed;
    private Text lastPoint;
    private Text totalPoint;

    private GameObject addAtkBtn;
    private GameObject addDefBtn;
    private GameObject addSpeedBtn;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        atk = transform.Find("atk").GetComponent<Text>();
        def = transform.Find("def").GetComponent<Text>();
        speed = transform.Find("speed").GetComponent<Text>();
        lastPoint = transform.Find("point").GetComponent<Text>();
        totalPoint = transform.Find("totalPoint").GetComponent<Text>();

        addAtkBtn = transform.Find("addAtk").gameObject;
        addDefBtn = transform.Find("addDef").gameObject;
        addSpeedBtn = transform.Find("addSpeed").gameObject;

        addAtkBtn.GetComponent<Button>().onClick.AddListener(AddAtkClick);
        addDefBtn.GetComponent<Button>().onClick.AddListener(AddDefClick);
        addSpeedBtn.GetComponent<Button>().onClick.AddListener(AddSpeedClick);
    }

    public override void Show()
    {
        base.Show();
        UpdateStatus();
    }

    void UpdateStatus()
    {
        atk.text = PlayerStatus.Instance.attack + "+" + PlayerStatus.Instance.attack_plus;
        def.text = PlayerStatus.Instance.def + "+" + PlayerStatus.Instance.def_plus;
        speed.text = PlayerStatus.Instance.speed + "+" + PlayerStatus.Instance.speed_plus;
        lastPoint.text = PlayerStatus.Instance.point_remain.ToString();
        totalPoint.text = (PlayerStatus.Instance.attack_plus + PlayerStatus.Instance.def_plus + 
            PlayerStatus.Instance.speed_plus).ToString();

        if (PlayerStatus.Instance.point_remain > 0)
        {
            addAtkBtn.SetActive(true);
            addDefBtn.SetActive(true);
            addSpeedBtn.SetActive(true);
        }
        else
        {
            addAtkBtn.SetActive(false);
            addDefBtn.SetActive(false);
            addSpeedBtn.SetActive(false);
        }
    }

    public void AddAtkClick()
    {
        bool success = PlayerStatus.Instance.AddPoint();
        if (success)
        {
            PlayerStatus.Instance.attack_plus++;
            UpdateStatus();
        }
    }

    public void AddDefClick()
    {
        bool success = PlayerStatus.Instance.AddPoint();
        if (success)
        {
            PlayerStatus.Instance.def_plus++;
            UpdateStatus();
        }
    }
    public void AddSpeedClick()
    {
        bool success = PlayerStatus.Instance.AddPoint();
        if (success)
        {
            PlayerStatus.Instance.speed_plus++;
            UpdateStatus();
        }
    }
}
