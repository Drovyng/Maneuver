using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[Serializable]
public class Level
{
    public bool playerLR;
    public string id;
    public List<(Vector2, float)> obstacles;
    public Vector4 finish;
    public float bestTime;
    public float mediumTime;
    
    public Level(string id, List<(Vector2, float)> obstacles, Vector4 finish, float bestTime, float mediumTime, bool playerLR)
    {
        this.id = id;
        this.obstacles = obstacles;
        this.finish = finish;
        this.bestTime = bestTime;
        this.mediumTime = mediumTime;
        this.playerLR = playerLR;
    }
    private static TextInfo ToTitle = new CultureInfo("en-US").TextInfo;
    public string GetName()
    {
        return GetName(id);
    }
    public static string GetName(string value)
    {
        return ToTitle.ToTitleCase(value.Replace("_", " "));
    }
    public static readonly List<Level> Levels = new()
    {
        new("tutorial", new(), new(0, 30f, 7, 1), 5.215f, 7.581f, false),

        new("warp", new(), new(0, 30f, 7, 1), 10, 15, true),
        new("warp_v2", new(), new(0, 30f, 7, 1), 10, 15, false),

        new("upside_down", new(), new(0, -30f, 7, 1), 8, 12, false),
        new("upside_down_v2", new(), new(0, -30f, 7, 1), 10, 15, false)
    };
    public static void LevelFeathuresStart(string id)
    {
        switch (id)
        {
            case "tutorial":
                ObstacleSpawner.SpawnDefault();
                Game.Instance.player.leftRight = UnityEngine.Random.Range(0, 2) == 0;
                return;
            case "warp_v2":
            case "warp":
                Game.Instance.bounds[0].gameObject.SetActive(false);
                Game.Instance.bounds[1].gameObject.SetActive(false);
                for (int i = 0; i < 5; i++)
                {
                    float y = i * 10 - 3;

                    ObstacleSpawner.Spawn(new Vector3(-2, y));
                    ObstacleSpawner.Spawn(new Vector3(-1.2f, 1 + y));
                    ObstacleSpawner.Spawn(new Vector3(-0.4f, 2 + y));
                    ObstacleSpawner.Spawn(new Vector3(0.4f, 3 + y));
                    ObstacleSpawner.Spawn(new Vector3(1.2f, 4 + y));
                    ObstacleSpawner.Spawn(new Vector3(2, 5 + y));
                    ObstacleSpawner.Spawn(new Vector3(1.2f, 6 + y));
                    ObstacleSpawner.Spawn(new Vector3(0.4f, 7 + y));
                    ObstacleSpawner.Spawn(new Vector3(-0.4f, 8 + y));
                    ObstacleSpawner.Spawn(new Vector3(-1.2f, 9 + y));

                    ObstacleSpawner.Spawn(new Vector3(-2f, 4 + y));
                    ObstacleSpawner.Spawn(new Vector3(-1.2f, 5 + y));
                    ObstacleSpawner.Spawn(new Vector3(-2f, 6 + y));
                    ObstacleSpawner.Spawn(new Vector3(2f, 9 + y));
                    ObstacleSpawner.Spawn(new Vector3(1.2f, y));
                    ObstacleSpawner.Spawn(new Vector3(2f, 1 + y));
                }
                return;
            case "upside_down_v2":
            case "upside_down":
                Game.Instance.notFollowPlayer = true;
                ObstacleSpawner.SpawnDefault();
                foreach (var obst in ObstacleSpawner.Instance.obstacles)
                {
                    obst.transform.position *= -1;
                }
                Game.Instance.player.leftRight = UnityEngine.Random.Range(0, 2) == 0;
                Game.Instance.bounds[2].transform.localPosition *= -1;
                return;
        }
    }
    public static void LevelFeathuresUpdate(string id)
    {
        switch (id)
        {
            case "warp_v2":
            case "warp":
                var plr = Game.Instance.player;
                if (plr.transform.position.x >= 2.85)
                {
                    plr.transform.position -= new Vector3(5.68f, 0, 0);
                }
                else if (plr.transform.position.x <= -2.85)
                {
                    plr.transform.position += new Vector3(5.68f, 0, 0);
                }
                return;
            case "upside_down_v2":
            case "upside_down":
                var g = Game.Instance;
                if (g.player.transform.position.y < g.transform.position.y + 3.5f)
                {
                    g.transform.position = new Vector3(0, Mathf.Lerp(g.transform.position.y, g.player.transform.position.y - 3.5f, Time.fixedDeltaTime * 2.75f));
                }
                return;
        }
    }
    public static int GetStarsByTime(string id, float time)
    {
        var level = Levels.Find((lol) => lol.id == id);
        if (time <= level.bestTime) return 3;
        if (time <= level.mediumTime) return 2;
        return 1;
    }
}
