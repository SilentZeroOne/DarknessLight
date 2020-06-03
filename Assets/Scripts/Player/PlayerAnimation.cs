using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;

    public Animator animator;
    private PlayerMovement movement;
    private PlayerAttack playerAttack;
    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isMoving", movement.isMoving);
        animator.SetBool("isNormalAttacking", playerAttack.isAttacking);
        animator.SetFloat("Attack", playerAttack.attackValue);
        animator.SetBool("isDead", PlayerStatus.Instance.isDead);
        animator.SetBool("Attack1", playerAttack.attack1);
        animator.SetBool("isTakeDamage", PlayerStatus.Instance.isTakeDamage);
        animator.SetBool("GroundImpact", playerAttack.groundImpact);
        animator.SetBool("MagicBall", playerAttack.magicBall);
        animator.SetBool("AttackCritical", playerAttack.attackCritical);
    }
}
