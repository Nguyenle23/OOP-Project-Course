using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    public float speed = 100f, maxspeed = 3, jumpPow = 220f;
    public bool Ground = true;
    
    public Rigidbody2D r2;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Ground" , Ground);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Ground)
                r2.AddForce(Vector2.up * jumpPow);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        r2.AddForce((Vector2.right) * speed * h);

        if (r2.velocity.x > maxspeed)
            r2.velocity = new Vector2(maxspeed, r2.position.y);
        if (r2.velocity.x < -maxspeed)
            r2.velocity = new Vector2(-maxspeed, r2.position.y);
    }
}
