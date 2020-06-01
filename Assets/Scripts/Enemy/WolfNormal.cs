using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfNormal : WolfBaby
{
    protected override void RandomAttack()
    {
        float val = Random.Range(0f, 1f);
        if (val <= crazyAttackRate)
        {
            animator.SetBool("Attack2", true);
            PlayerStatus.Instance.GetDamage(attack * 1.5f);
            Invoke("SetAttack2", 0.867f);
        }
        else
        {
            animator.SetBool("Attack1", true);
            PlayerStatus.Instance.GetDamage(attack);
            Invoke("SetAttack1", 0.633f);
        }
    }
}
