using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4;
    private PlayerDir dir;
    private CharacterController controller;
    public bool isMoving;

    void Start()
    {
        dir = GetComponent<PlayerDir>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float distance = Vector3.Distance(dir.targetPos, transform.position);
        if (distance > 0.1f)
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
