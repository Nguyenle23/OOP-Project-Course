using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Megaman : Player
{
    [SerializeField]
    private float shootDelay = .1f;

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    Transform BulletSpawnPos;

    private bool isShooting;

    public object position { get; internal set; }

    [SerializeField]
    private ManaUI mana;

    [SerializeField]
    private float maxMana = 5;

    // Start is called before the first frame update
    protected override void Start()
    {
        anim = GetComponent<Animator>();
        r2 = gameObject.GetComponent<Rigidbody2D>();
        ourHealth = maxhealth;
        mana.Initialize(maxMana, maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Ground", Ground);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Ground)
            {
                Ground = false;
                doublejump = true;
                r2.AddForce(Vector2.up * jumpPow * 10);
            }
            else if (doublejump)
            {
                doublejump = false;
                r2.velocity = new Vector2(r2.velocity.x, 0);
                r2.AddForce(Vector2.up * jumpPow * 5f);
            }
            FindObjectOfType<AudioManager>().Play("MegamanJump");
        }
        
        if ((Input.GetKey(KeyCode.E) && mana.MyCurrentValue >= 1))
        {
            if (isShooting) return;

            //shoot
            anim.Play("Shooting");
            isShooting = true;

            //shoot when Megaman is idle or walk or jump
            if (r2.velocity.x > 0 )
            {
                anim.Play("WalkShoot");
            }
            else if (r2.velocity.x < 0)
            {
                anim.Play("WalkShoot");
            }
            if (Input.GetKey(KeyCode.E) && (Input.GetKey(KeyCode.Space)) && Ground == false)
            {
                anim.Play("JumpShoot");
            }

            //instantiate and shoot bullet
            GameObject bullet; 
            bullet = Instantiate(Bullet);
            bullet.GetComponent<Bullet>().StartShoot(faceright);
            bullet.transform.position = BulletSpawnPos.transform.position;
            
            //Flip the image of bullet
            if (faceright == true)
            {
                bullet.transform.position = BulletSpawnPos.transform.position;
            }
            else
            {
                bullet.transform.position = BulletSpawnPos.transform.position;
                bullet.transform.Rotate(1f, 0f, 180);
            }

            Invoke("ResetShoot", shootDelay); //Delay Megaman shoot
            mana.MyCurrentValue -= 1f; //Decrease mana Megaman
            FindObjectOfType<AudioManager>().Play("MegamanShoot");
        }

        //Fill Mana
        if (Input.GetKeyDown(KeyCode.R))
        {
            mana.MyCurrentValue += 1;
        }

        //Fill Health
        if (Input.GetKeyDown(KeyCode.F))
        {
            ourHealth += 1;
        }

        //Improving Megaman jump
        if (r2.velocity.y < 0)
        {
            r2.velocity += Vector2.up * Physics2D.gravity.y * (fallgravity - 1) * Time.deltaTime;
        }
        else if (r2.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            r2.velocity += Vector2.up * Physics2D.gravity.y * (upgravity - 1) * Time.deltaTime;
        }
    }

    public void ResetShoot()    //Function delay Megaman shoot
    {
        isShooting = false;
        anim.Play("Idle");
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        r2.AddForce((Vector2.right)* speed * h);

        if (r2.velocity.x > maxspeed)
            r2.velocity = new Vector2(maxspeed, r2.velocity.y);
        if (r2.velocity.x < -maxspeed)
            r2.velocity = new Vector2(-maxspeed, r2.velocity.y);

        if (r2.velocity.y > maxjump)
            r2.velocity = new Vector2(r2.velocity.x, maxjump);
        if (r2.velocity.y < -maxjump)
            r2.velocity = new Vector2(r2.velocity.x, -maxjump);

        if (h > 0 && !faceright)
        {
            Flip();
        }
        if (h < 0 && faceright)
        {
            Flip();
        }
        if (Ground)
        {
            r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
        }

        if (ourHealth <= 0)
        {
            FindObjectOfType<AudioManager>().Play("MegamanDeath");
            Destroy(gameObject);
        }
    }

    public void Damage(int damage) //Function Megaman damaged
    {
        ourHealth -= damage;
        gameObject.GetComponent<Animation>().Play("RedFlash");
        FindObjectOfType<AudioManager>().Play("MegamanDamage");
    }

    private void Flip() //Function flip the image of Megaman
    {
        faceright = !faceright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

