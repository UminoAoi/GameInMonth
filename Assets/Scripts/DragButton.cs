using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DragButton : MonoBehaviour, IDragHandler
{
    public Vector3 position;

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
            SceneManager.LoadScene("LevelGray");
        }
        else if(-transform.position.x > transform.localScale.x / 2)//only color
        {
            Debug.Log("PINK");
            SceneManager.LoadScene("LevelColor");
        }
        else
        {
            Debug.Log("BOTH");
            SceneManager.LoadScene("LevelBoth");
        }
    }
}
