using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3;
    private PlayerDir dir;
    private CharacterController controller;
    public bool isMoving;
    public PlayerAttack attack;
    //public AudioSource stepSource;
    //public AudioClip[] stepClips;

    void Start()
    {
        dir = GetComponent<PlayerDir>();
        controller = GetComponent<CharacterController>();
        attack = GetComponent<PlayerAttack>();
        //stepSource = gameObject.AddComponent<AudioSource>();
        
    }

    void Update()
    {
        if (PlayerStatus.Instance.isDead) return;
        if (attack.state == PlayerState.ControlWalk&&!PlayerStatus.Instance.isDead)
        {
            float distance = Vector3.Distance(dir.targetPos, transform.position);
            if (distance > 0.2f)
            {
                controller.SimpleMove(transform.forward * speed);
                isMoving = true;
                //PlayStepClip();
            }
            else
            {
                isMoving = false;
            }
        }
      
    }
    public void SimpleMove(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
        controller.SimpleMove(transform.forward * speed);
        //PlayStepClip();
    }

    //private void PlayStepClip()
    //{
    //    int i = Random.Range(0, stepClips.Length );
    //    stepSource.clip = stepClips[i];
    //    stepSource.Play();
    //}
}
