using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    private Rigidbody2D birdBody;
    private int limiteIzquierdoDeMapa = -13;
    private int limiteDerechoDeMapa = 42;

    void Start()
    {
        birdBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        birdBody.velocity = new Vector2(Random.Range(-3, -5), 0);
        if (transform.position.x < limiteIzquierdoDeMapa)
        {
            transform.position = new Vector3(limiteDerechoDeMapa, Random.Range(8,5), 0); 
        }
    }
}
