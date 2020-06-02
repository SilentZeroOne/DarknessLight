using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDir : MonoBehaviour
{
    public GameObject clickFXPrefab;
    private PlayerMovement movement;

    private Vector3 offset = new Vector3(0, 0.4f, 0);
    private bool isMoving;
    private bool isCollider;
    private RaycastHit hit;
    [HideInInspector]
    public Vector3 targetPos;
    private void Awake()
    {
        targetPos = transform.position;
        movement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        ClickGround();
    }

    private void ClickGround()
    {
        if (PlayerStatus.Instance.isDead) return;
        if (Input.GetMouseButtonDown(1)&&!EventSystem.current.IsPointerOverGameObject())
        {
            GroundRayCheck(out isCollider, out hit);
            if (isCollider && hit.collider.tag == TagsManager.ground)
            {
                isMoving = true;
                ShowEffect(hit.point);                           
                ChangeDir(hit.point);
            }
        }
        if (isMoving)
        {
            GroundRayCheck(out isCollider, out hit);
            if (isCollider && hit.collider.tag == TagsManager.ground)
            {
                ChangeDir(hit.point);
            }
        }
        if (movement.isMoving)
        {
            ChangeDir(hit.point);
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMoving = false;
        }
    }

    private void GroundRayCheck(out bool isCollider,out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        isCollider = Physics.Raycast(ray, out hit);
    }

    private void ShowEffect(Vector3 hitPos)
    {
        Instantiate(clickFXPrefab, hitPos+offset, Quaternion.identity);
    }

    private void ChangeDir(Vector3 hitPos)
    {
        targetPos = hitPos;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }
}
