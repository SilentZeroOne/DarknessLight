using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawn : MonoBehaviour
{
    public int maxNum = 5;
    private int currentNum = 0;
    public float time = 10;
    private float timer = 0;
    public float minRange = 0;
    public float maxRange = 0;

    public GameObject prefab;

    private void Update()
    {
        if (currentNum < maxNum)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                Vector3 pos = transform.position;
                pos.x += Random.Range(minRange,maxRange);
                pos.z += Random.Range(minRange, maxRange);
                GameObject obj= Instantiate(prefab, pos, Quaternion.identity);
                obj.GetComponent<WolfBaby>().spawn = this;
                timer = 0;
                currentNum++; 
            }
        }
    }

    public void MinusNum()
    {
        currentNum--;
    }
}
