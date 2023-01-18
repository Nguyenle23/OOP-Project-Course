using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public Megaman player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Megaman>();
    }

    void FixedUpdate()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.x = player.transform.position.x;
        mat.mainTextureOffset = offset * Time.fixedDeltaTime / 0.42f;
    }
}