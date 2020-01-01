using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float jumpSpeed = 10f;
    PlayerType playerType;
    public float throwingForce = 8;
    public Transform holdingPosition;
    public ParticleSystem blood;

    int life = 3;
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

        if (objectThing.CompareTag("Boss"))
        {
            Die();
        }

        if ((objectThing.layer == 11 && playerType == PlayerType.GRAY) ||
                (objectThing.layer == 10 && playerType == PlayerType.PINK))
        {
            if (life == 1)
                Die();
            Hurt();
            Destroy(objectThing);
        }
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
        life--;
        rb.velocity = new Vector2(15, 3);
        StartCoroutine(Blink());
    }

    void Die()
    {
        //Camera shake
        StartCoroutine(Blink());
        rb.velocity = new Vector2(15, 3);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator Blink()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color oldColor = sprite.color;
        sprite.color = Color.red;
        blood.Play();
        yield return new WaitForSeconds(0.3f);
        sprite.color = oldColor;
        blood.Stop();
    }

    public PlayerType GetPlayerType()
    {
        return playerType;
    }
}

public enum PlayerType
{
    GRAY,
    PINK
}
