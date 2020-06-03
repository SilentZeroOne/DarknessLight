
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WolfState
{
    Walk,
    Idle,
    Attack,
    Death
}

public class WolfBaby : MonoBehaviour
{
    protected Animator animator;
    private CharacterController controller;
    private new Renderer renderer;
    public WolfState state;
    public float speed = 1;
    public float hp = 100;
    public int attack = 10;
    public int exp = 20;
    public Transform target;
    public float maxFollowDistance = 10;
    public float attackDistance=2;
    public float miss_rate = 0.2f;

    private float time=1;
    private float timer = 0;
    

    public bool isMiss;
    public bool isDead;
    private bool inBattle;
    public AudioClip missClip;
    public GameObject damageNumPrefab;
    public Transform damageNumPos;

    public int attackRate = 1;
    private float attackTimer = 0;
    public float crazyAttackRate = 0.3f;

    public WolfSpawn spawn;
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        renderer = GetComponentInChildren<Renderer>();
    }

    void Update()
    {
        if (state == WolfState.Walk)
        {
            animator.SetBool("isWalk", true);
            controller.SimpleMove(transform.forward*speed);
        }
       // else if (state==WolfState.Attack)
        //{
            AutoAttack();
        //}
        timer += Time.deltaTime;
        if (!inBattle&&timer >= time)
        {
            timer = 0;
            RandomState();
        }
    }

    void AutoAttack()
    {
        
        if (target != null &&!PlayerStatus.Instance.isDead)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            inBattle = true;
            if (distance > maxFollowDistance)
            {
                target = null;
                state = WolfState.Idle;
            }
            else if (distance<=attackDistance)
            {
                animator.SetBool("isWalk", false);
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackRate&&!isDead)
                {
                    state = WolfState.Attack;
                    transform.LookAt(target);
                    RandomAttack();                  
                    attackTimer = 0;
                }
            }
            else
            {
                transform.LookAt(target);
                state = WolfState.Walk;
              //  AutoAttack();
            }
        }
        else
        {
            inBattle = false;
        }
    }

   protected virtual void RandomAttack()
    {
        float val = Random.Range(0f, 1f);
        if (val <= crazyAttackRate)
        {
            animator.SetBool("Attack2", true);
            PlayerStatus.Instance.GetDamage(attack * 1.5f);
            Invoke("SetAttack2", 0.733f);
        }
        else
        {
            animator.SetBool("Attack1", true);
            PlayerStatus.Instance.GetDamage(attack);
            Invoke("SetAttack1", 0.633f);
        }
    }

    void RandomState()
    {
        int value = Random.Range(0, 2);
        if (value == 0)
        {
            state = WolfState.Idle;
            animator.SetBool("isWalk", false);
        }
        else
        {
            transform.Rotate(transform.up * Random.Range(0, 360));
            state = WolfState.Walk;
        }
      
    }
    public void TakeDamage(float attack)
    {
        if (state == WolfState.Death) return;
        target = GameObject.FindGameObjectWithTag(TagsManager.player).transform;
        state = WolfState.Attack;
        animator.SetBool("isWalk", false);
        float value = Random.Range(0f, 1f);
        if (value <= miss_rate)
        {
            //miss
            isMiss = true;
            Instantiate(damageNumPrefab, damageNumPos.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(missClip, transform.position);
            Invoke("SetMiss", 1f);
        }
        else
        {

            hp -= attack;
            GameObject obj = Instantiate(damageNumPrefab, damageNumPos.position, Quaternion.identity);
            obj.GetComponent<DamageNumber>().Value =attack;
            StartCoroutine(ShowBabyBodyRed());
            animator.SetFloat("takeDamage",(float)attack/hp);
            animator.SetBool("isTakeDamage", true);
            if((float)attack / hp>0.5f)
             Invoke("SetTakeDamage", 1f);
            else Invoke("SetTakeDamage", 0.6f);
            if (hp <= 0)
            {
                OnDeath();
            }
        }
    }

    private void OnMouseEnter()
    {
        if(!PlayerAttack.instance.isLockingTarget)
        CursorManager.instance.SetAttack();
    }
    private void OnMouseExit()
    {
        if(!PlayerAttack.instance.isLockingTarget)
        CursorManager.instance.SetNormal();
    }
    private IEnumerator ShowBabyBodyRed()
    {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(1);
        renderer.material.color = Color.white;
    }
    private void OnDeath()
    {
        state = WolfState.Death;
        animator.SetBool("isDead", true);
        animator.SetBool("isWalk", false);
        isDead = true;
        spawn.MinusNum();
        controller.enabled = false;
        if (BarNpc.instance.isInTask)
        {
            BarNpc.instance.killCount++;
        }
        PlayerStatus.Instance.GetExp(exp);
        Destroy(gameObject, 2f);
    }

    private void SetMiss()
    {
        isMiss = false;
    }
    private void SetTakeDamage()
    {
        animator.SetBool("isTakeDamage", false);
        animator.SetFloat("takeDamage", 0f);
    }

    void SetAttack1()
    {
        animator.SetBool("Attack1", false);
    }
    void SetAttack2()
    {
        animator.SetBool("Attack2", false);
    }
}
