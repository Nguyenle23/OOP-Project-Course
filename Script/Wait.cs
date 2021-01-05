using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{
    //Call variable to set the time waiting
    public float wait_Time = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait_for_intro());
    }

    //Create an method wait_for_intro
    IEnumerator Wait_for_intro()
    {
        yield return new WaitForSeconds(wait_Time);

        SceneManager.LoadScene(1); //Load next scene
    }
}
