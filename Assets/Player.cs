using UnityEngine;

public class Player : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public bool leftRight;
    private void Start()
    {
        leftRight = Random.Range(0, 2) == 0;
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && Game.gameStarted && !Game.gameFailed)
        {
            Game.PlaySound(0);
            leftRight = !leftRight;
            const float velX = 3f;
            const float velY = 10f;

            rigidbody.velocity = new Vector2(leftRight ? -velX : velX, velY);
            rigidbody.angularVelocity = (leftRight ? velX : -velX) * 180;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Game.GameFail();
    }
}
