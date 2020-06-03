using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad : MonoBehaviour
{
    public GameObject[] prefabs;

    private void Awake()
    {
        int index= PlayerPrefs.GetInt("selectedCharacterIndex");
        if (index == 0)
        {
            Instantiate(prefabs[0], transform.position, transform.rotation);
        }
        else
        {
            Instantiate(prefabs[1], transform.position, transform.rotation);
        }
    }
}
