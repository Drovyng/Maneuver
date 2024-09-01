using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    public List<Obstacle> obstacles = new();

    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private GameObject _obstaclesParent;

    private void Awake()
    {
        Instance = this;
    }
    public static void SpawnDefault()
    {
        for (float i = 0; i < 25; i += 2.5f)
        {
            Spawn(new Vector3(0, 6 + i));
        }
    }
    public static void Spawn(Vector3 pos, float scale = 1)
    {
        var obj = Instantiate(
            Instance._obstaclePrefab,
            pos, 
            Quaternion.identity, 
            Instance._obstaclesParent.transform
        );
        var comp = obj.GetComponent<Obstacle>();
        Instance.obstacles.Add(comp);
        comp.Spawn();
        obj.transform.localScale *= scale;
    }
}
