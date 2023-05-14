using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool isSpawned;
    private bool leftRight;
    private bool scored;
    //public Transform mini;
    private void Start()
    {
        Respawn();
        isSpawned = true;
    }
    private void Respawn()
    {
        transform.position = new Vector3(Random.Range(0, 4) * 1.25f - 2, isSpawned ? transform.position.y + 25 : transform.position.y);
        //mini.position = transform.position;
        scored = false;
        leftRight = Random.Range(0, 2) == 0;
    }
    private void FixedUpdate()
    {
        if (transform.position.y <= Game.instance.transform.position.y - 10)
        {
            Respawn();
        }
        if (!Game.gameFailed && !scored && transform.position.y + 1.25f <= Game.instance.player.transform.position.y)
        {
            scored = true;
            Game.score += 1;
            Game.PlaySound(Game.score % 10 != 0 ? 1 : 3);
        }
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + (leftRight ? 180 : -180) * Time.fixedDeltaTime * Game.timeScale);
        //mini.rotation = Quaternion.Euler(0, 0, mini.rotation.eulerAngles.z + (leftRight ? 360 : -360) * Time.fixedDeltaTime * Game.timeScale);
    }
}
