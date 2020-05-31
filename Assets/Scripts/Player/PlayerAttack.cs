using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    ControlWalk,
    NormalAtk,
    SkillAtk,

}
public class PlayerAttack : MonoBehaviour
{
    public PlayerState state = PlayerState.ControlWalk;
    private Animator animator;
    private PlayerMovement movement;

    public float normalAtkTime = 1;
    private float attackTimer = 0;
    public float minAtkDistance = 5;

    public bool isAttacking;
    public Transform normalAttackTarget;

    private bool atkIt;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit);
            if (isCollider && hit.collider.tag == TagsManager.enemy)
            {
                normalAttackTarget = hit.collider.transform;
                state = PlayerState.NormalAtk;
            }
            else
            {
                state = PlayerState.ControlWalk;
                normalAttackTarget = null;
                isAttacking = false;
            }
        }

        if (state == PlayerState.NormalAtk&& normalAttackTarget!=null)
        {
            float distance = Vector3.Distance(transform.position, normalAttackTarget.position);
            if (distance <= minAtkDistance)
            {
                //TODO 攻击
                movement.isMoving = false;
                transform.LookAt(normalAttackTarget.position);
                attackTimer += Time.deltaTime;
                if (attackTimer >= normalAtkTime)
                {
                    isAttacking = true;
                    StartCoroutine(NormalAttack());
                    //normalAttackTarget.GetComponent<WolfBaby>().TakeDamage(PlayerStatus.Instance.attack);
                    //Invoke("SetNormalAttack", 0.8f);
                }
            }
            else
            {
                movement.SimpleMove(normalAttackTarget.position);
                movement.isMoving = true;
            }
        }
    }
    void SetNormalAttack()
    {
        isAttacking = false;

        attackTimer = 0;
    }
    IEnumerator NormalAttack()
    {


        yield return new WaitForSeconds(0.8f);
        SetNormalAttack();
    }
}
