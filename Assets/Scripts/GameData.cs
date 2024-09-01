using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class LevelScoreData
{
    public string id;
    public float time;
    public int attempts;
    public LevelScoreData(string id, float time, int attempts)
    {
        this.id = id;
        this.time = time;
        this.attempts = attempts;
    }
}

[Serializable]
public class GameData
{
    public static readonly GameData Instance = GetGameData();
    private static GameData GetGameData()
    {
        if (PlayerPrefs.HasKey("GameData"))
        {
            return JsonUtility.FromJson<GameData>(PlayerPrefs.GetString("GameData"));
        }
        PlayerPrefs.DeleteAll();
        return new();
    }
    public static void Save()
    {
        PlayerPrefs.SetString("GameData", JsonUtility.ToJson(Instance));
        PlayerPrefs.Save();
    }
    public float totalTime;
    public int color;
    public int attempts;
    public int totalAttempts;
    public int maxScore;
    public int lastScore;
    public int totalScore;
    public List<LevelScoreData> levels = new();
}
