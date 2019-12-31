using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 100;
    public float attackTime = 5;
    public GameObject player;
    public MonsterType monsterType = MonsterType.BOTH;
    public List<GameObject> bullets;
    public float throwingForce = 20;

    GameObject nextBullet;
    int currentBullet = 0;


    float timeSinceAttack;
    bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        nextBullet = bullets[0];
        timeSinceAttack = 0;
        isRight = true;
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

        if (transform.position.x - player.transform.position.x > 0 && isRight)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isRight = false;
        }
        else if (transform.position.x - player.transform.position.x < 0 && !isRight)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isRight = true;
        }

        Vector2 target = new Vector2(player.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
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

}

public enum MonsterType
{
    GRAY,
    PINK,
    BOTH
}
