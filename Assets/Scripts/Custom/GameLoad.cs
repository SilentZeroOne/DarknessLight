using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad : MonoBehaviour
{
    public static GameLoad instance;
    public GameObject[] prefabs;
    private int index;
    private GameObject player;
    private void Awake()
    {
        instance = this;
        index= PlayerPrefs.GetInt("selectedCharacterIndex");
        player=Instantiate(prefabs[index], transform.position, transform.rotation);
    }

    public void Reborn()
    {
        player.transform.position = transform.position;
        PlayerStatus.Instance.ReBorn();
    }
}
