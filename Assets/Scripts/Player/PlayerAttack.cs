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
    private Skill skill;

    public float normalAtkTime = 1;
    public float attackTimer = 0;
    public float minAtkDistance = 5;
    public float normalAttackRate = 2;

    public bool isAttacking;
    public float attackValue;
    public float finalAtk;
    public float addAtk=1;

    public bool attack1;
    public bool groundImpact;
    public bool magicBall;
    public bool attackCritical;

    public Transform normalAttackTarget;
    private bool isFirst;
    public bool isLockingTarget;

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
        if (PlayerStatus.Instance.isDead) return;
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit);
            if (isCollider && hit.collider.tag == TagsManager.enemy&&state != PlayerState.NormalAtk)
            {
                normalAttackTarget = hit.collider.transform;
                isFirst = true;
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
                if (!isFirst)
                {
                    if (attackTimer >= normalAtkTime)
                    {
                        isAttacking = false;
                        attackValue = 0;
                        if (!showEffect)
                        {
                            showEffect = true;
                            Instantiate(normalAtkeffect, normalAttackTarget.position, Quaternion.identity);
                            normalAttackTarget.GetComponent<WolfBaby>().TakeDamage(finalAtk*GetAttack()*addAtk);

                        }
                    }
                }
                if (attackTimer >= (1f / normalAttackRate))
                {
                    attackTimer = 0;
                    showEffect = false;
                    isAttacking = true;
                    isFirst = false;
                    RandomAttack();

                    if (normalAttackTarget.GetComponent<WolfBaby>().isDead)
                    {
                        isAttacking = false;
                        attackValue = 0;
                    }
                }
            }
            else
            {
                movement.SimpleMove(normalAttackTarget.position);
                movement.isMoving = true;
            }
        }
        if (isLockingTarget && Input.GetMouseButtonDown(0))
        {
            OnLockTarget();
        }
    }
    public int GetAttack()
    {
        return EquipmentPanel.instance.totalAtk + PlayerStatus.Instance.attack + PlayerStatus.Instance.attack_plus;
    }
    public void UseSkill(Skill skill)
    {

        switch (skill.applyType)
        {
            case ApplyType.Buff:
                StartCoroutine(OnBuffSkillUse(skill));
                break;
            case ApplyType.Passive:
               StartCoroutine(OnPassiveSkillUse(skill));
                break;
            case ApplyType.SingleTarget:
                OnSingleSkillUse(skill);
                break;
            case ApplyType.MultiTarget:
                OnSingleSkillUse(skill);
                break;
        }
    }
    IEnumerator OnBuffSkillUse(Skill skill)
    {
        state = PlayerState.SkillAtk;
        if (skill.animName== "Skill-GroundImpact")
        {
            groundImpact = true;
            yield return new WaitForSeconds(skill.duration);
            Instantiate(efxDict[skill.efxName], transform.position, Quaternion.identity);
            groundImpact = false;
            state = PlayerState.ControlWalk;
            switch (skill.applyProperty)
            {
                case ApplyProperty.Attack:
                    StartCoroutine(AddATK(skill));
                    break;
                case ApplyProperty.AttackSpeed:
                    StartCoroutine(AddAtkSpeed(skill));
                    break;
            }
        }
    }
    IEnumerator AddATK(Skill skill)
    {
        addAtk = skill.applyValue / 100 * addAtk;
        yield return new WaitForSeconds(skill.applyTime);
        addAtk = addAtk / skill.applyValue * 100;
    }
    IEnumerator AddAtkSpeed(Skill skill)
    {
        normalAttackRate *= skill.applyValue / 100 ;
        movement.speed *= skill.applyValue / 100;
        animator.SetFloat("Speed", 1);
        yield return new WaitForSeconds(skill.applyTime);
        normalAttackRate = normalAttackRate/skill.applyValue * 100;
        movement.speed /= skill.applyValue / 100;
        animator.SetFloat("Speed", 0);
    }

    IEnumerator OnPassiveSkillUse(Skill  skill)
    {
        state = PlayerState.SkillAtk;
        if (skill.animName == "Attack1")
        {
            attack1 = true;

            yield return new WaitForSeconds(skill.duration);
            switch (skill.applyProperty)
            {
                case ApplyProperty.HP:
                    PlayerStatus.Instance.UseDrug(skill.applyValue, 0);
                    break;
                case ApplyProperty.MP:
                    PlayerStatus.Instance.UseDrug(0, skill.applyValue);
                    break;
            }
            Instantiate(efxDict[skill.efxName], transform.position, Quaternion.identity);
            attack1 = false;
            state = PlayerState.ControlWalk;
        }
    }

    void OnSingleSkillUse(Skill skill)
    {
        state = PlayerState.SkillAtk;
        CursorManager.instance.SetLockTarget();
        isLockingTarget = true;
        this.skill = skill;

    }
    void OnLockTarget()
    {
        isLockingTarget = false;
        switch (skill.applyType)
        {
            case ApplyType.SingleTarget:
                StartCoroutine(OnSingleTarget());
                break;
            case ApplyType.MultiTarget:
                StartCoroutine(OnMultiTarget());
                break;
        }
    }
    IEnumerator OnSingleTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit);
        if (isCollider && hit.collider.tag == TagsManager.enemy)
        {
            if (skill.animName == "Skill-MagicBall")
            {
                magicBall = true;
                transform.LookAt(hit.collider.transform);
                yield return new WaitForSeconds(skill.duration);
                Instantiate(efxDict[skill.efxName], hit.collider.transform.position, Quaternion.identity);
                float bounsAtk = skill.applyValue / 100;
                hit.collider.GetComponent<WolfBaby>().TakeDamage(addAtk*GetAttack() * bounsAtk);
                bounsAtk = 0;
                magicBall = false;
                //state = PlayerState.ControlWalk;
            }
        }
        else
        {
           
            CursorManager.instance.SetNormal();
            PlayerStatus.Instance.mp_remain += skill.costMp;
        }
        state = PlayerState.ControlWalk;
    }
    IEnumerator OnMultiTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit);
        if (isCollider &&hit.collider.tag == TagsManager.ground)
        {
            float distance = Vector3.Distance(transform.position,hit.point);
            if (distance <= minAtkDistance*2)
            {
                if (skill.animName == "AttackCritical")
                {
                    attackCritical = true;
                    transform.LookAt(hit.collider.transform);
                    yield return new WaitForSeconds(skill.duration);
                    GameObject obj = Instantiate(efxDict[skill.efxName], hit.point + Vector3.up * 0.5f, Quaternion.identity);
                    float bounsAtk = skill.applyValue / 100;
                    obj.GetComponent<MagicSphere>().atk = addAtk * GetAttack() * bounsAtk;
                    attackCritical = false;

                }
            }
            else
            {
                Debug.Log("超过最远施法距离");
                PlayerStatus.Instance.mp_remain += skill.costMp;
            }
        }
        else
        {
            PlayerStatus.Instance.mp_remain += skill.costMp;
        }
        state = PlayerState.ControlWalk;
        CursorManager.instance.SetNormal();
    }


    void RandomAttack()
    {
        attackValue = Random.Range(0f, 1f);
        if (attackValue <= 0.8f)
        {
            finalAtk = 1;
        }
        else
        {
            finalAtk *= 1.5f;
        }
    }
}
