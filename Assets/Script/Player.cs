using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 50f, maxspeed = 3, jumpPow = 300f, maxjump = 4;
    public bool Ground = true, faceright = true, doublejump = false;
    public float fallgravity = 2.5f;
    public float upgravity = 2f;

    public Rigidbody2D r2;
    public Animator anim;

    public int ourHealth;
    public int maxhealth = 10;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    { 

    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Knockback(float Knockpow, Vector2 Knockdir)
    {
        r2.velocity = new Vector2(0, 0);
        r2.AddForce(new Vector2(Knockdir.x * -40, Knockdir.y * Knockpow));
    }
}
