﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragButton : MonoBehaviour, IDragHandler
{
    public Vector3 position;
    public 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(position.x, position.y, 0);
    }

    public void OnClick()
    {
        if(transform.position.x > transform.localScale.x / 2)//only gray
        {
            Debug.Log("GRAY");
        }
        else if(-transform.position.x > transform.localScale.x / 2)//only color
        {
            Debug.Log("PINK");
        }
        else
        {
            Debug.Log("BOTH");
        }
    }
}
