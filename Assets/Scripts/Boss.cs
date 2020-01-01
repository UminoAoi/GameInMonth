using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 100;
    public float attackTime = 5;
    public MonsterType monsterType = MonsterType.BOTH;
    public List<GameObject> bullets;
    public float throwingForce = 20;
    public List<Transform> places;

    int life = 10;
    GameObject nextBullet;
    int currentBullet = 0;

    Transform nextPlace;
    int currentPlace = 0;

    float timeSinceAttack;
    bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        nextBullet = bullets[0];
        timeSinceAttack = 0;
        isRight = true;
        nextPlace = places[0];
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;

        if(timeSinceAttack > attackTime)
        {
            timeSinceAttack = 0;
            Attack();
        }

        if (transform.position.x - nextPlace.position.x > 0 && isRight)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isRight = false;
        }
        else if (transform.position.x - nextPlace.position.x < 0 && !isRight)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isRight = true;
        }

        Vector2 target = new Vector2(nextPlace.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, nextPlace.position) < 0.3)
        {
            if(places.Count > currentPlace + 1)
            {
                nextPlace = places[++currentPlace];
            }
            else
            {
                nextPlace = places[0];
            }
        }
    }

    void Attack()
    {
        nextBullet.transform.position = transform.position;
        GameObject bullet = Instantiate(nextBullet);
        if(bullet.layer == 11)// różowy
        {
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = new Vector2(transform.localScale.x, throwingForce);
        }

        if(bullets.Count > currentBullet+1)
        {
            nextBullet = bullets[++currentBullet];
        }
        else
        {
            currentBullet = 0;
            nextBullet = bullets[0];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objectThing = collision.gameObject;

        if (objectThing.layer == 8 )
        {
            if (life == 1)
                Die();
            Hurt();
            Destroy(objectThing);
        }
    }

    void Hurt()
    {

    }

    void Die()
    {

    }

}

public enum MonsterType
{
    GRAY,
    PINK,
    BOTH
}
