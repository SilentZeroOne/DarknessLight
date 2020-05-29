using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int level = 1;
    public int coins = 200;
    public int hp = 100;//最大值
    public int hp_remain = 100;
    public int mp = 100;//最大值
    public int mp_remain = 100;
    public string playerName="Default";
    public int attack = 20;
    public int attack_plus = 0;
    public int def = 20;
    public int def_plus = 0;
    public int speed = 20;
    public int speed_plus = 0;

    public int point_remain=0;//剩余的点数
    public Job job = Job.Magician;


    private static PlayerStatus instance;
    public static PlayerStatus Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
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
}
