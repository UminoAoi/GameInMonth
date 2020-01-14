using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackTime = 5;
    public List<GameObject> bullets;
    public float throwingForce = 20;
    public ParticleSystem blood;
    public int life = 1;
    public List<GameObject> prizes;

    GameObject nextBullet;
    int currentBullet = 0;

    float timeSinceAttack;
    public EnemyType type;

    // Start is called before the first frame update
    void Start()
    {
        nextBullet = bullets[0];
        timeSinceAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;
        if (IfAttack(attackTime))
            Attack();
    }

    public bool IfAttack(float attackTime)
    {
        if (timeSinceAttack > attackTime)
            return true;
        else
            return false;
    }

    public void Attack()
    {
        timeSinceAttack = 0;
        nextBullet.transform.position = transform.position;
        GameObject bullet = Instantiate(nextBullet);
        if (bullet.layer == 11)// różowy
        {
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = new Vector2(transform.localScale.x, throwingForce);
        }

        TakeNextBullet();

    }

    public void TakeNextBullet()
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
        CollideWithObject(objectThing);
    }

    public void CollideWithObject(GameObject objectThing)
    {
        if (objectThing.layer == 8) { 
            if((objectThing.CompareTag("CanInteractPINK") && type== EnemyType.GRAY)|| 
               (objectThing.CompareTag("CanInteractGRAY") && type == EnemyType.PINK))
            {
                if (life == 1)
                    Die();
                Hurt();
                Destroy(objectThing);
            }
        }
    }

    public void Hurt()
    {

        life--;
        StartCoroutine(Blink());
    }

    void Die()
    {
        //Camera shake
        StartCoroutine(BlinkDead());
    }

    public IEnumerator Blink()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color oldColor = sprite.color;
        sprite.color = Color.red;
        blood.Play();
        yield return new WaitForSeconds(0.3f);
        sprite.color = oldColor;
        blood.Stop();
    }

    IEnumerator BlinkDead()
    {
        attackTime = float.MaxValue;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color oldColor = sprite.color;
        for (int i = 0; i < 4; i++)
        {
            sprite.color = Color.red;
            blood.Play();
            yield return new WaitForSeconds(0.5f);
            sprite.color = oldColor;
            blood.Stop();
        }
        PushOutPrizes();
        Destroy(gameObject);

    }

    public void PushOutPrizes()
    {
        foreach (GameObject prize in prizes)
        {
            prize.transform.position = transform.position;
            GameObject prizeObject = Instantiate(prize);
            Rigidbody2D prizeRb = prizeObject.GetComponent<Rigidbody2D>();
            int random = Random.Range(-2, 2);
            prizeRb.velocity = new Vector2(transform.localScale.x * random, 3);
        }
    }
}

public enum EnemyType
{
    PINK,
    GRAY,
    NONE
}
