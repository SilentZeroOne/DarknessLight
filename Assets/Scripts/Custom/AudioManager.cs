using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public GameObject[] buttons;
    public AudioClip[] audioClips;
    public int useMusic;
    private static AudioManager instance;
    
    public static AudioManager Instance
    {
        get { return instance; }
    }
    
    private void Awake()
    {
        Init();
    }


    private void Init()
    {
        instance = this;
        transform.gameObject.AddComponent<AudioSource>();
        AudioSource bgm = transform.gameObject.GetComponent<AudioSource>();
        bgm.clip = audioClips[1];
        bgm.loop = true;
        bgm.Play();
        if (buttons.Length != 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].AddComponent<AudioSource>();
                buttons[i].GetComponent<AudioSource>().clip = audioClips[useMusic];
                buttons[i].GetComponent<AudioSource>().playOnAwake = false;
                //buttons[i].GetComponent<Button>().onClick.AddListener(()=>PlayButtonClip(i));
            }
        }
     
    }

    public void PlayButtonClip(int i)
    {
        buttons[i].GetComponent<AudioSource>().Play();
    }
}
