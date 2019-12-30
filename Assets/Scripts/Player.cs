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
    bool isRight;
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
        isRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        if (direction < 0 && isRight)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isRight = false;
        }else if(direction > 0 && !isRight)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isRight = true;
        }

        float move = direction * speed * Time.deltaTime;

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
                DropGray();
            }
            else
            {
                DropPink();
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

        if ((objectThing.layer == 11 && playerType == PlayerType.GRAY) ||
                (objectThing.layer == 10 && playerType == PlayerType.PINK))
        {
            Hurt();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject objectThing = collision.gameObject;

            if ((objectThing.CompareTag("CanInteractGRAY") && playerType == PlayerType.GRAY) || 
                (objectThing.CompareTag("CanInteractPINK") && playerType == PlayerType.PINK))
            {
                if (Input.GetKeyDown("space") && !isHolding)
                {
                    PickItem(objectThing);
                }
            }
    }

    void DropGray()
    {
        holdingObject.transform.parent = null;
        holdingObject.GetComponent<Rigidbody2D>().simulated = true;
        holdingObject = null;
        isHolding = false;
    }

    void DropPink()
    {
        holdingObject.transform.parent = null;
        Rigidbody2D objectRb = holdingObject.GetComponent<Rigidbody2D>();
        objectRb.simulated = true;
        objectRb.velocity = new Vector2(transform.localScale.x, throwingForce);
        holdingObject = null;
        isHolding = false;
    }

    void PickItem(GameObject objectThing)
    {
        objectThing.transform.parent = transform;
        holdingObject = objectThing;
        isHolding = true;
        holdingObject.GetComponent<Rigidbody2D>().simulated = false;
        holdingObject.layer = 8;
        objectThing.transform.position = holdingPosition.position;
    }

    void Hurt() { 
    }

}

public enum PlayerType
{
    GRAY,
    PINK
}
