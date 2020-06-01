using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBoss : WolfBaby
{
    private float criticalRate = 0.2f;
    protected override void RandomAttack()
    {
        float val = Random.Range(0f, 1f);
       
        if (val <= criticalRate)
        {
            animator.SetBool("CriticalAtk", true);
            PlayerStatus.Instance.GetDamage(attack * 2);
            Invoke("SetCritical", 1.3f);
        }
        else if (val <= crazyAttackRate)
        {
            animator.SetBool("Attack2", true);
            PlayerStatus.Instance.GetDamage(attack * 1.5f);
            Invoke("SetAttack2", 1.133f);
        }
        else
        {
            animator.SetBool("Attack1", true);
            PlayerStatus.Instance.GetDamage(attack);
            Invoke("SetAttack1", 0.967f);
        }
    }
    void SetCritical()
    {
        animator.SetBool("CriticalAtk", false);
    }
}
