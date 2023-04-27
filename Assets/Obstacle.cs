using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool isSpawned;
    private bool leftRight;
    private bool scored;
    private void Start()
    {
        leftRight = Random.Range(0, 2) == 0;
        Respawn();
        isSpawned = true;
    }
    private void Respawn()
    {
        transform.position = new Vector3(Random.Range(0, 4) * 1.25f - 2, isSpawned ? transform.position.y + 25 : transform.position.y);
        scored = false;
    }
    private void FixedUpdate()
    {
        if (transform.position.y <= Game.instance.transform.position.y - 10)
        {
            Respawn();
        }
        if (!Game.gameFailed && !scored && transform.position.y + 0.75f <= Game.instance.player.transform.position.y)
        {
            scored = true;
            Game.score += 1;
            Game.PlaySound(1);
        }
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + (leftRight ? 180 : -180) * Time.fixedDeltaTime * Game.timeScale);
    }
}
