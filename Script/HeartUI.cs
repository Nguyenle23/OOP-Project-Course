using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public Sprite[] Heartsprite;

    public Megaman player;

    public Image Heart;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Megaman>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.ourHealth > 10)
            player.ourHealth = 10;

        if (player.ourHealth < 0)
            player.ourHealth = 0;

        Heart.sprite = Heartsprite[player.ourHealth];
    }
}
