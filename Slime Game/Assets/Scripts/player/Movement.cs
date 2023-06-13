using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Movement : MonoBehaviour
{
    [SerializeField] private float salto = 14;
    [SerializeField] private float peso = 8;
    [SerializeField] private TrailRenderer trailRenderer; 

    private Rigidbody2D player;
    private SpriteRenderer spritePlayer;
    private float directionX;
    private bool puedeSaltarNuevamente;

    private bool puedeHacerDash = true;
    private bool estaHaciendoDash;
    private float dashPower = 24f;
    private float tiempoDash = 0.2f;
    private float dashCoolDown = 1f;


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
        if (estaHaciendoDash)
        {
            return;
        }

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

        if (Input.GetKeyDown(KeyCode.LeftShift) && puedeHacerDash)
        {
            StartCoroutine(Dash());
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

    private IEnumerator Dash()
    {
        puedeHacerDash = false;
        estaHaciendoDash = true;
        float originalGravity = player.gravityScale;
        player.gravityScale = 0f;

        float impulso;
        if (spritePlayer.flipX == false)
        {
            impulso = -(transform.localScale.x * dashPower);
        }
        else
        {
            impulso = transform.localScale.y * dashPower;
        }
        player.velocity = new Vector2(impulso, 0f);

        trailRenderer.emitting = true;
        yield return new WaitForSeconds(tiempoDash);
        trailRenderer.emitting = false;

        player.gravityScale = originalGravity;
        estaHaciendoDash = false;
        yield return new WaitForSeconds(dashCoolDown);
        puedeHacerDash = true;
    }
}
