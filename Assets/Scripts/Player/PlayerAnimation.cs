using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isMoving", movement.isMoving);
    }
}
