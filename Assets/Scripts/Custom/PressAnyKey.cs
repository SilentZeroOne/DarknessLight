using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    public GameObject buttons;
    private bool isAnyKeyPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAnyKeyPressed)
        {
            if (Input.anyKey)
            {
                ShowButton();
            }
        }
       
    }

    private void ShowButton()
    {
        buttons.SetActive(true);
        transform.gameObject.SetActive(false);
        isAnyKeyPressed = true;
    }
}
