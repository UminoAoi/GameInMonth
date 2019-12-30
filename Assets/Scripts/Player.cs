using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float jumpSpeed = 10f;
    PlayerType playerType;
    public float throwingForce = 10;

    public Transform holdingPosition;
    Rigidbody2D rb;
    bool isGround;
    bool isHolding;
    GameObject holdingObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (this.CompareTag("PlayerPINK"))
        {
            playerType = PlayerType.PINK;
        }
        else
        {
            playerType = PlayerType.GRAY;
        }

        isGround = false;
        isHolding = false;
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        rb.velocity = new Vector2(move, rb.velocity.y);

        if (Input.GetKeyDown("up") && isGround)
        {
            rb.AddForce(new Vector2(0, jumpSpeed));
            isGround = false;
        }


        if (Input.GetKeyDown("z") && isHolding) //upuszczenie rzeczy
        {
            if (playerType == PlayerType.GRAY)
            {
                holdingObject.transform.parent = null;
                holdingObject.GetComponent<Rigidbody2D>().simulated = true;
                holdingObject = null;
                isHolding = false;
            }
            else
            {
                Debug.Log("CAN");
                holdingObject.transform.parent = null;
                Rigidbody2D objectRb = holdingObject.GetComponent<Rigidbody2D>();
                objectRb.simulated = true;
                objectRb.velocity = new Vector2(transform.localScale.x, throwingForce);
                holdingObject = null;
                isHolding = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objectThing = collision.gameObject;

        if(objectThing.CompareTag("Platform"))
            isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject objectThing = collision.gameObject;

        if (objectThing.CompareTag("Platform"))
            isGround = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject objectThing = collision.gameObject;

            if ((objectThing.CompareTag("CanInteractGRAY") && playerType == PlayerType.GRAY) || 
                (objectThing.CompareTag("CanInteractPINK") && playerType == PlayerType.PINK))
            {
                if (Input.GetKeyDown("space") && !isHolding)
                {
                    Debug.Log(holdingPosition);
                    objectThing.transform.parent = transform;
                    holdingObject = objectThing;
                    isHolding = true;
                    holdingObject.GetComponent<Rigidbody2D>().simulated = false;
                    objectThing.transform.position = holdingPosition.position;
                }
            }
    }

}

public enum PlayerType
{
    GRAY,
    PINK
}
