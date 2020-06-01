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
    public static PlayerAttack instance;

    public float normalAtkTime = 1;
    public float attackTimer = 0;
    public float minAtkDistance = 5;
    public float normalAttackRate = 2;

    public bool isAttacking;
    public Transform normalAttackTarget;

    // private bool atkIt;
    public bool showEffect;
    public GameObject normalAtkeffect;
    public GameObject[] efxArray;

    public Dictionary<string, GameObject> efxDict=new Dictionary<string, GameObject>();
    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();

        foreach (var item in efxArray)
        {
            efxDict.Add(item.name, item);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit);
            if (isCollider && hit.collider.tag == TagsManager.enemy&&state != PlayerState.NormalAtk)
            {
                normalAttackTarget = hit.collider.transform;
                state = PlayerState.NormalAtk;
                showEffect = false;
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
                    isAttacking = false;
                    if (!showEffect)
                    {
                        showEffect = true;
                        Instantiate(normalAtkeffect, normalAttackTarget.position, Quaternion.identity);
                        normalAttackTarget.GetComponent<WolfBaby>().TakeDamage(GetAttack());

                    }
                }
                if (attackTimer >= (1f / normalAttackRate))
                {
                    attackTimer = 0;
                    showEffect = false;
                    isAttacking = true;
                    if (normalAttackTarget.GetComponent<WolfBaby>().isDead)
                    {
                        isAttacking = false;
                    }
                }
            }
            else
            {
                movement.SimpleMove(normalAttackTarget.position);
                movement.isMoving = true;
            }
        }
    }
    public int GetAttack()
    {
        return EquipmentPanel.instance.totalAtk + PlayerStatus.Instance.attack + PlayerStatus.Instance.attack_plus;
    }
    public void UseSkill(Skill skill)
    {
        state = PlayerState.SkillAtk;
        switch (skill.applyType)
        {
            case ApplyType.Buff:
                OnBuffSkillUse();
                break;
            case ApplyType.Passive:
                OnPassiveSkillUse(skill);
                break;
        }
    }
    void OnBuffSkillUse()
    {

    }
    void OnPassiveSkillUse(Skill  skill)
    {

    }
}
