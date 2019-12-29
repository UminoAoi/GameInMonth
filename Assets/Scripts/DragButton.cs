using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragButton : MonoBehaviour, IDragHandler
{
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
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        transform.position = position;
    }
}
