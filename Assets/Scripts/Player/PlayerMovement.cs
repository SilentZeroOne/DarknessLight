﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4;
    private PlayerDir dir;
    private CharacterController controller;
    public bool isMoving;
    public PlayerAttack attack;

    void Start()
    {
        dir = GetComponent<PlayerDir>();
        controller = GetComponent<CharacterController>();
        attack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if (attack.state == PlayerState.ControlWalk)
        {
            float distance = Vector3.Distance(dir.targetPos, transform.position);
            if (distance > 0.2f)
            {
                controller.SimpleMove(transform.forward * speed);
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }
      
    }
    public void SimpleMove(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
        controller.SimpleMove(transform.forward * speed);
    }
}
