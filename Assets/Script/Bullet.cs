using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed = 20;

    [SerializeField]
    int damage;

    [SerializeField]
    float timeToDestroy = 0.3f;

    public Rigidbody2D r2;
    public bool faceright = true;

    public void StartShoot(bool faceright)
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        if (faceright == true)
        {
            r2.velocity = new Vector2(speed, 0);
        }
        else
        {
            r2.velocity = new Vector2(-speed, 0);
        }

        Destroy(gameObject, timeToDestroy);
    }

    void FixedUpdate()
    { 
        if (faceright !=true)
        {
            Flip();
        }
    }

    private void Flip()
    {  
        faceright = !faceright;
        transform.Rotate(1f, 0f, 180);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            Destroy(gameObject, timeToDestroy);
        }
}}
