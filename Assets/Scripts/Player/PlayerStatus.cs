using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int level = 1;
    public float exp = 0; //当前获得经验   
    public float totalExp=130;

    public int coins = 200;
    public int hp = 100;//最大值
    public float hp_remain = 100;
    public int mp = 100;//最大值
    public int mp_remain = 100;
    public string playerName="Default";
    public int attack = 20;
    public int attack_plus = 0;
    public int def = 20;
    public int def_plus = 0;
    public int speed = 20;
    public int speed_plus = 0;
    public float missRate;
    public bool isDead;
    public bool isTakeDamage;

    public int point_remain=0;//剩余的点数
    public Job job = Job.Magician;

    public Transform damageNumPos;
    public GameObject damageNumPrefab;
    public AudioClip missClip;
    private new Renderer renderer;

    private static PlayerStatus instance;
    public static PlayerStatus Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
        renderer = GetComponentInChildren<Renderer>(); 
    }
    private void Start()
    {
        GetExp(0);
    }


    public void GetExp(float exp)
    {
        this.exp += exp;
        totalExp = level * 30 + 100;
        while (this.exp >= totalExp)
        {
            level++;
            this.exp -= totalExp;
            totalExp = level * 30 + 100;
        }
        ExpBar.instance.SetValue(this.exp / totalExp);
    }

    public void GetCoins(int addCoin)
    {
        coins += addCoin;
    }

    public bool AddPoint(int point = 1)
    {
        if (point_remain >= point)
        {
            point_remain -= point;
            return true;
        }
        return false;
    }

    public void UseDrug(int hp,int mp)
    {
        hp_remain += hp;
        mp_remain += mp;
        if (hp_remain > this.hp)
            hp_remain = this.hp;
        if (mp_remain > this.mp)
            mp_remain = this.mp;
    }
    public bool CostMP(int mp)
    {
        if ((mp_remain - mp) >= 0)
        {
            mp_remain -= mp;
            return true;
        }
        return false;
    }
    public void GetDamage(float damage)
    {
        float totalDef = EquipmentPanel.instance.totalDef + def + def_plus;
        float totalSpeed = EquipmentPanel.instance.totalSpeed + speed + speed_plus;
        missRate = totalSpeed / 200;
        float tempDamage = damage * ((200 - totalDef) / 200);
        GameObject obj;
        float val = Random.Range(0f, 1f);
        if (val <= missRate)
        {
            obj= Instantiate(damageNumPrefab, transform.position + Vector3.up, Quaternion.identity);
            obj.GetComponent<DamageNumber>().Value = 0;
            AudioSource.PlayClipAtPoint(missClip, transform.position);
        }
        else
        {

            if (tempDamage <= 1) tempDamage = 1;
            hp_remain -= tempDamage;
            StartCoroutine(ShowRedBody());
            isTakeDamage = true;
            PlayerAnimation.instance.animator.SetFloat("takeDamage", damage / hp);
            StartCoroutine(TakeDamage());
            obj = Instantiate(damageNumPrefab, transform.position + Vector3.up, Quaternion.identity);
            obj.GetComponent<DamageNumber>().Value = (int)tempDamage;
            if (hp_remain <= 0) isDead = true;
        }

    }
    private IEnumerator ShowRedBody()
    {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(1);
        renderer.material.color = Color.white;
    }
    private IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(0.6f);
        isTakeDamage = false;
        PlayerAnimation.instance.animator.SetFloat("takeDamage", 0);
    }



}
