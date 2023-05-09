using UnityEngine;

public class ScoreTen : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (transform.position.y <= Game.instance.transform.position.y - 10)
        {
            transform.position += Vector3.up * 25;
        }
    }
}
