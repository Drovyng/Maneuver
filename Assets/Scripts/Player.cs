using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    public bool leftRight;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();

        leftRight = Random.Range(0, 2) == 0;
    }
    public void Recolor() => _spriteRenderer.color = Utils.ColorPlayer;

    public void Jump()
    {
        _rigidbody.simulated = true;
        Game.PlaySound(0);
        leftRight = !leftRight;
        const float velX = 3f;
        const float velY = 10f;

        _rigidbody.velocity = new Vector2(leftRight ? -velX : velX, velY);
        _rigidbody.angularVelocity = (leftRight ? velX : -velX) * 180;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Game.GameFail();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish") && !Game.gameFailed)
        {
            Game.LevelComplete();
        }
    }
}
