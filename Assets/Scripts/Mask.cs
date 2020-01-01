using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    bool touching;
    bool changeStarted;
    bool moving;

    
    public float maxScale = 5f;
    public float scaleStep = 0.01f;
    public float rotationSpeed = 25f;
    public float moveSpeed = 10f;

    public GameObject triangle = null;
    public MaskType maskType = MaskType.PINK;
    public List<Transform> movingPlaces;

    Transform startingPlace;
    Transform nextPlace;
    int currentPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        touching = false;
        changeStarted = false;
        if (maskType == MaskType.PINK)
            moving = true;
        else if (maskType == MaskType.GRAY)
            moving = false;

        if (movingPlaces.Count >= 2)
        {
            startingPlace = movingPlaces[0];
            nextPlace = movingPlaces[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (maskType != MaskType.WHITE)
        {
            if (moving)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPlace.position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, nextPlace.position) < 0.3)
                {
                    if (movingPlaces.Count == currentPosition + 1)
                    {
                        nextPlace = movingPlaces[0];
                        currentPosition = 0;
                    }
                    else
                    {
                        nextPlace = movingPlaces[++currentPosition];
                    }
                }
            }

            if (touching && triangle.transform.localScale.x < maxScale)
            {
                triangle.transform.localScale += new Vector3(scaleStep, scaleStep);
                Vector3 rotateVector = new Vector3(0, 0, rotationSpeed * Time.deltaTime);
                triangle.transform.Rotate(rotateVector);
            }

            if (triangle.transform.localScale.x >= maxScale)
            {
                if (maskType == MaskType.PINK)
                    moving = false;
                else if (maskType == MaskType.GRAY)
                    moving = true;
            }
        }
            

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objectThing = collision.gameObject;

        if (!changeStarted )
        {
            triangle.transform.position = collision.gameObject.transform.position;
            changeStarted = true;
        }

        if((objectThing.CompareTag("PlayerGRAY") && maskType == MaskType.PINK) ||
           (objectThing.CompareTag("PlayerPINK") && maskType == MaskType.GRAY))
            touching = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touching = false;
    }
}

public enum MaskType
{
    PINK,
    GRAY,
    WHITE
}
