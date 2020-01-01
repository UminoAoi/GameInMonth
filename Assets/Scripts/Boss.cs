using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float normalSpeed = 100;
    public float attackTime = 5;
    public List<GameObject> bullets;
    public float throwingForce = 20;
    public List<Transform> places;
    public Player player;
    public ParticleSystem blood;
    public int angryAttacks = 4;
    public int life = 10;

    MonsterState monsterState;
    int currentAngryAttacks = 0;
    
    GameObject nextBullet;
    int currentBullet = 0;

    Transform nextPlace;
    int currentPlace = 0;

    float timeSinceAttack;
    bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        monsterState = MonsterState.Normal;
        nextBullet = bullets[0];
        timeSinceAttack = 0;
        isRight = true;
        nextPlace = places[0];
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;

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

        switch (monsterState)
        {
            case MonsterState.Normal:
                if (IfAttack(attackTime))
                    Attack();
                Move(normalSpeed);
                break;
            case MonsterState.Angry:
                if(currentAngryAttacks >= angryAttacks && IfAttack(attackTime / 2))
                {
                    currentAngryAttacks = 0;
                    Attack();
                }else if (IfAttack(attackTime/2))
                    AttackPlayer();
                Move(normalSpeed * 2);
                break;
            case MonsterState.Tired:
                if (IfAttack(attackTime))
                    Attack();
                Move(normalSpeed / 2);
                break;
        }

        if (Vector2.Distance(transform.position, nextPlace.position) < 0.3)
        {
            if(places.Count > currentPlace + 1)
            {
                nextPlace = places[++currentPlace];
            }
            else
            {
                nextPlace = places[0];
                currentPlace = 0;
            }
        }

    }

    void Move(float speed)
    {
        Vector2 target = new Vector2(nextPlace.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    bool IfAttack(float attackTime)
    {
        if (timeSinceAttack > attackTime)
            return true;
        else
            return false;
    }

    void Attack()
    {
        timeSinceAttack = 0;
        nextBullet.transform.position = transform.position;
        GameObject bullet = Instantiate(nextBullet);
        if(bullet.layer == 11)// różowy
        {
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = new Vector2(transform.localScale.x, throwingForce);
        }

        TakeNextBullet();
        
    }

    void AttackPlayer()
    {
        timeSinceAttack = 0;
        PlayerType type = player.GetPlayerType();
        GameObject bullet = null;
        for(int i = 0; i< bullets.Count; i++)
        {
            if ((type == PlayerType.GRAY && bullets[i].CompareTag("CanInteractPINK")) ||
                (type == PlayerType.PINK && bullets[i].CompareTag("CanInteractGRAY")))
            {
                bullet = bullets[i];
                currentBullet = i;
                break;
            }
        }
        
        bullet.transform.position = transform.position;
        bullet = Instantiate(bullet);
        if (bullet.layer == 11)// różowy
        {
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = new Vector2(transform.localScale.x, throwingForce);
        }

        currentAngryAttacks++;
        TakeNextBullet();
    }

    void TakeNextBullet()
    {
        if (bullets.Count > currentBullet + 1)
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
        life--;
        StartCoroutine(Blink());
        if (life % 3 == 1)
            monsterState = MonsterState.Normal;
        else if (life % 3 == 0)
            monsterState = MonsterState.Angry;
        else if (life % 3 == 2)
            monsterState = MonsterState.Tired;
    }

    void Die()
    {
        //Camera shake
        StartCoroutine(BlinkDead());
    }

    IEnumerator Blink()
    {
        float tmp = normalSpeed;
        normalSpeed = 0;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color oldColor = sprite.color;
        sprite.color = Color.red;
        blood.Play();
        yield return new WaitForSeconds(0.3f);
        sprite.color = oldColor;
        blood.Stop();
        normalSpeed = tmp;
    }

    IEnumerator BlinkDead()
    {
        normalSpeed = 0;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color oldColor = sprite.color;
        for(int i = 0; i<4; i++)
        {
            sprite.color = Color.red;
            blood.Play();
            yield return new WaitForSeconds(0.5f);
            sprite.color = oldColor;
            blood.Stop();
        }
        Destroy(gameObject);
        
    }

}

public enum MonsterState
{
    Normal,
    Angry,
    Tired
}
