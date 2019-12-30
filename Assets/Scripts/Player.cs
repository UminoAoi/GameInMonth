using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float jumpSpeed = 10f;

    Rigidbody2D rb;
    bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGround = false;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
}
