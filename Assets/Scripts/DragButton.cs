using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DragButton : MonoBehaviour, IDragHandler
{
    public Vector3 position;
    public Animator animator;

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
            StartCoroutine(LoadScene("LevelGray"));
        }
        else if(-transform.position.x > transform.localScale.x / 2)//only color
        {
            Debug.Log("PINK");
            StartCoroutine(LoadScene("LevelColor"));
        }
        else
        {
            Debug.Log("BOTH");
            StartCoroutine(LoadScene("LevelBoth"));
        }
    }

    public IEnumerator LoadScene(string scene)
    {
        animator.SetTrigger("Out");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
