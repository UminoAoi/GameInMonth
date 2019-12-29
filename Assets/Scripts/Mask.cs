using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public GameObject triangle;
    bool touching;

    // Start is called before the first frame update
    void Start()
    {
        touching = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (touching)
        {
            triangle.transform.localScale += new Vector3(0.01f, 0.01f);
            Vector3 rotateVector = new Vector3(0, 0, 25 * Time.deltaTime);
            triangle.transform.Rotate(rotateVector);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touching = true;
    }
}
