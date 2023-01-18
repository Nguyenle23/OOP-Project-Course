using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int Levelload = 1;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SceneManager.LoadScene(Levelload);
        }
    }

}
