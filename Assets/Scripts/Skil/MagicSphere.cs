using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    public float atk = 0;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagsManager.enemy)
        {
            other.GetComponent<WolfBaby>().TakeDamage(atk);
        }
    }
}
