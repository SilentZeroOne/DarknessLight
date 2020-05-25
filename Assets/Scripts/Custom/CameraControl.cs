using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    private CinemachineVirtualCamera vCamera;
    private CinemachineTransposer cinemachineTransposer;
    private PlayerMovement movement;
    public float distance;
    private float maxDistance = 18;
    private float minDistance = 3;
    public float scrollSpeed = 5;

    private bool isRotating;
    public float rotateSpeed = 5;

    void Start()
    {
        vCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineTransposer = vCamera.GetCinemachineComponent<CinemachineTransposer>();
        movement = GameObject.FindGameObjectWithTag(TagsManager.player).GetComponent<PlayerMovement>();
    }

  
    void Update()
    {
        
        RotateView();
        ScrollView();
    }

    private void ScrollView()
    {
        distance = cinemachineTransposer.m_FollowOffset.magnitude;
        distance-=Input.GetAxis("Mouse ScrollWheel")*scrollSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        cinemachineTransposer.m_FollowOffset = cinemachineTransposer.m_FollowOffset.normalized * distance;
    }
    private void RotateView()
    {
        if (Input.GetMouseButtonDown(0) && !movement.isMoving && !EventSystem.current.IsPointerOverGameObject())
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }
        if (isRotating && !movement.isMoving)
        {
            Vector3 originPos = transform.position;
            Quaternion originRoa = transform.rotation;
            transform.RotateAround(vCamera.Follow.position,vCamera.Follow.up, rotateSpeed * Input.GetAxis("Mouse X"));
            transform.RotateAround(vCamera.Follow.position, transform.right, rotateSpeed * Input.GetAxis("Mouse Y"));
            float x = transform.eulerAngles.x;
            if (x >= 85 || x <= 10)
            {
                transform.position = originPos;
                transform.rotation = originRoa;
            }


            cinemachineTransposer.m_FollowOffset = transform.position - vCamera.Follow.position;
        }
        
    }
}
