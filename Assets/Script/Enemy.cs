using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float agroRange = 12;

    [SerializeField]
    float moveSpeed = 8;

    [SerializeField]
    int enemyHealth;

    public Megaman player;

    public Rigidbody2D r2;
    public Animator anim;

    public bool faceright = true;
    public float distanceToLeft, distanceToRight;
    public float distance;
    public float wakerange;
    public Transform shootpointL, shootpointR;

    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Megaman>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        distanceToLeft = Vector2.Distance(shootpointL.position, player.transform.position);
        distanceToRight = Vector2.Distance(shootpointR.position, player.transform.position);
        if (distance < agroRange)
        {
            if (transform.position.x < player.transform.position.x) 
            {
                isChasing();
                Invoke("ChasingPlayerLeft", 0.5f);
            }
            else 
            {
                isChasing();
                Invoke("ChasingPlayerRight", 0.5f);
            }
        }

        if (distance > agroRange)
        {
            //stop chasing
            StopChasingPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            enemyHealth = enemyHealth - 1;
            FindObjectOfType<AudioManager>().Play("EnemyDamage");
            if (distance > agroRange)
            {
                gameObject.GetComponent<Animation>().Play("EnemyIdleRed");
            }
            else if (distance < agroRange)
            {
                gameObject.GetComponent<Animation>().Play("EnemyRotateRedFlash");
            }
            if (enemyHealth == 0)
            {
                FindObjectOfType<AudioManager>().Play("EnemyDeath");
                Destroy(gameObject);
            }
        }
        else if (col.CompareTag("Player"))
        {
            player.Damage(2);
            player.Knockback(350f, player.transform.position);
        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(99);
        yield return 0;
    }

    public void ChasingPlayerLeft()
    {
        if (transform.position.x < player.transform.position.x)
        {
            FindObjectOfType<AudioManager>().Play("EnemyChase");
            r2.velocity = new Vector2(moveSpeed, 0);
           
            transform.localScale = new Vector2(-1, 1);
        }
        anim.Play("EnemyRotate");
    }

    public void ChasingPlayerRight()
    {
        FindObjectOfType<AudioManager>().Play("EnemyChase");
        transform.localScale = new Vector2(1, 1);
        r2.velocity = new Vector2(-moveSpeed, 0);
        anim.Play("EnemyRotate");
    }
    public void isChasing()
    {
        
    }
    public void StopChasingPlayer()
    {
        r2.velocity = new Vector2(0, 0);
        anim.Play("EnemyIdle");
    }

    public void Damage(int damage)
    {
        enemyHealth -= damage;
    }

    public void Knockback(float Knockpow, Vector2 Knockdir)
    {
        r2.velocity = new Vector2(0, 0);
        r2.AddForce(new Vector2(Knockdir.x * -10, Knockdir.y * Knockpow));
    }

    private void Flip()
    {
        faceright = !faceright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
