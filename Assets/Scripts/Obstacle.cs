using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool isSpawned;
    private bool leftRight;
    private bool scored;


    public void Spawn()
    {
        Respawn();
        GetComponent<SpriteRenderer>().color = Utils.ColorObstacle;
        isSpawned = true;
    }
    private void Respawn()
    {
        if (Game.level == null) transform.position = new Vector3(Random.Range(0, 4) * 1.25f - 2, isSpawned ? transform.position.y + 25 : transform.position.y);
        
        scored = false;
        leftRight = Random.Range(0, 2) == 0;
    }
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + (leftRight ? 180 : -180) * Time.fixedDeltaTime * Game.timeScale);
        if (Game.level != null) return;
        if (transform.position.y <= Game.Instance.transform.position.y - 10)
        {
            Respawn();
        }
        if (!Game.gameFailed && !scored && transform.position.y + 1.25f <= Game.Instance.player.transform.position.y)
        {
            scored = true;
            Game.score += 1;

            Game.PlaySound((Game.score == GameData.Instance.lastScore || Game.score == GameData.Instance.maxScore) ? 3 : 1);
        }
    }
}
