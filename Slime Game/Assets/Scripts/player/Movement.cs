using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float salto = 14;
    [SerializeField] private float peso = 8;

    private Rigidbody2D player;
    private SpriteRenderer spritePlayer;
    private float directionX;
    private bool puedeSaltarNuevamente;


    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        spritePlayer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerAction();
        UpdateAnimation();
    }

    private void PlayerAction()
    {
        directionX = Input.GetAxisRaw("Horizontal"); 
        player.velocity = new Vector2(directionX * peso, player.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (puedeSaltarNuevamente)
            {
                player.velocity = new Vector2(player.velocity.x, salto);
                puedeSaltarNuevamente = false;
            }
        }
    }

    private void UpdateAnimation()
    {
        if (directionX != 0f)
        {
            if (directionX < 0f)
            {
                spritePlayer.flipX = false;
            }
            else
            {
                spritePlayer.flipX = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            puedeSaltarNuevamente = true;
        }
    }
}
