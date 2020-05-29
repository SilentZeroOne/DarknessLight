using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int distance = 10;
    protected GameObject player;
    protected float distanceBetweenPlayer;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(TagsManager.player);
    }
    protected virtual void Update()
    {
        distanceBetweenPlayer = Vector3.Distance(transform.position, player.transform.position);
    }
    private void OnMouseEnter()
    {
        CursorManager.instance.SetNpcTalk();
    }

    private void OnMouseExit()
    {
        CursorManager.instance.SetNormal();
    }
}
