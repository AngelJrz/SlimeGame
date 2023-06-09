using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float salto = 0;

    private Rigidbody2D player;
    private SpriteRenderer spritePlayer;
    private float directionX;


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
        player.velocity = new Vector2(directionX * 7f, player.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.velocity = new Vector2(player.velocity.x, salto);
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
}
