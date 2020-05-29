using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    public int zoomVal=2;
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + target.position;
    }

    public void ZoomIn()
    {
        if (transform.position.y > 36)
        {
            transform.position += new Vector3(0, zoomVal, 0);
            offset = offset - new Vector3(0, zoomVal, 0);
        }

    }
    public void ZoomOut()
    {
        if (transform.position.y < 48)
        {
            transform.position -= new Vector3(0, zoomVal, 0);
            offset = offset + new Vector3(0, zoomVal, 0);
        }

    }
}
