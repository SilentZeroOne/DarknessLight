using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollView : MonoBehaviour
{
    public static ScrollView instance;
    private CinemachineFollowZoom zoom;
    private CinemachineFreeLook freeLook;
    private float maxDistance = 24;
    private float minDistance = 2;
    public float scrollSpeed = 5;


    private void Awake()
    {
        instance = this;
        zoom = GetComponent<CinemachineFollowZoom>();
        freeLook = GetComponent<CinemachineFreeLook>();

    }
    private void Start()
    {
        ReFollow();
    }
    void Update()
    {
        Rotate();
        Scroll();
    }

    void Scroll()
    {
        zoom.m_Width -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        zoom.m_Width = Mathf.Clamp(zoom.m_Width, minDistance, maxDistance);
    }
    void Rotate()
    {
        if (Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
        {
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else if (Input.GetMouseButtonUp(0))
        {
            freeLook.m_XAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisName = "";
        }
    }
    public void ReFollow()
    {
        freeLook.Follow = GameObject.FindGameObjectWithTag(TagsManager.player).transform;
        freeLook.LookAt = GameObject.FindGameObjectWithTag(TagsManager.lookAtPos).transform;
    }
}
