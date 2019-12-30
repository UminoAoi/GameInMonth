using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float jumpSpeed = 10f;
    public PlayerType playerType = PlayerType.GRAY;

    Rigidbody2D rb;
    public bool isGround;
    bool isHolding;
    GameObject holdingObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (Input.GetKeyDown("z") && isHolding)
        {
            holdingObject.transform.parent = null;
            holdingObject.GetComponent<Rigidbody2D>().simulated = true;
            holdingObject = null;            
            isHolding = false;
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

        if (!objectThing.CompareTag("Untagged"))
        {
            if (playerType == PlayerType.GRAY && objectThing.CompareTag("CanInteractGRAY"))//podnoszenie rzeczy
            {
                if (Input.GetKeyDown("space") && !isHolding)
                {
                    objectThing.transform.parent = transform;
                    holdingObject = objectThing;
                    isHolding = true;
                    holdingObject.GetComponent<Rigidbody2D>().simulated = false;
                    objectThing.transform.localPosition = new Vector2(transform.localScale.x/4,0);
                }
            }
            else if (playerType == PlayerType.PINK && objectThing.CompareTag("CanInteractPINK"))//kopanie, wprawianie rzeczy w ruch
            {

            }
        }
    }

}

public enum PlayerType
{
    GRAY,
    PINK
}
