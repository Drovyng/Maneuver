using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage_Stats : MenuPage
{
    [SerializeField]
    private GameObject _statCellPrefab;
    private static readonly List<string> Names = new(){
        "Total Time",
        "Total Attempts",
        "Attempts",
        "Total Score",
        "Max Score",
        "Last Score",
        "Average Score",
        "Levels Completed",
        "Levels 3 Star",
        "Levels 2 Star",
        "Levels 1 Star"
    };
    private readonly List<MenuPage_Stats_Cell> Cells = new();
    private float _lerp;
    public override void Create()   // 210y 1940h   <=> 1180 -760
    {
        for (int i = 0; i < Names.Count; i++)
        {
            var cell = Instantiate(_statCellPrefab, transform);
            var component = cell.GetComponent<MenuPage_Stats_Cell>();
            Cells.Add(component);
            component._name.text = Names[i];
            cell.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 980 - i*1690/Names.Count);
        }
        Opening(0);
    }
    private int levelsStars3;
    private int levelsStars2;
    private int levelsStars1;
    public override void Open()
    {
        _lerp = 0;
        foreach (var level in GameData.Instance.levels)
        {
            var star = Level.GetStarsByTime(level.id, level.time);
            if (star > 0) levelsStars1++;
            if (star > 1) levelsStars2++;
            if (star > 2) levelsStars3++;
        }
        Opening(0);
    }
    private string GetValue(int i, float lerp)
    {
        switch (i)
        {
            case 0:
                int time = (int)(GameData.Instance.totalTime * lerp);
                int seconds = time % 60;
                int minutes = time / 60;
                int hours = time / 3600;
                return hours+"h "+(minutes>9?"":"0")+minutes+"m "+(seconds>9?"":"0")+seconds+"s";
            case 1:
                return "" + (int)(GameData.Instance.totalAttempts*lerp);
            case 2:
                return "" + (int)(GameData.Instance.attempts * lerp);
            case 3:
                return "" + (int)(GameData.Instance.totalScore * lerp);
            case 4:
                return "" + (int)(GameData.Instance.maxScore * lerp);
            case 5:
                return "" + (int)(GameData.Instance.lastScore * lerp);
            case 6:
                if (GameData.Instance.attempts < 1) return "0";
                return "" + (int)(GameData.Instance.totalScore * lerp / GameData.Instance.attempts);
            case 7:
                return "" + (int)(GameData.Instance.levels.Count * lerp);
            case 8:
                return "" + (int)(levelsStars3 * lerp);
            case 9:
                return "" + (int)(levelsStars2 * lerp);
            case 10:
                return "" + (int)(levelsStars1 * lerp);
            default:
                return "0";
        }
    }
    public override void Opening(float elapsed)
    {
        if (_lerp < Names.Count)
        {
            _lerp += elapsed;
            if (_lerp > Names.Count) _lerp = Names.Count;

            for (int i = 0; i < Names.Count; i++)
            {
                float lerp = Mathf.Min(Mathf.Max(_lerp - i * 0.25f, 0), 1);
                var col = new Color(1, 1, 1, lerp * 2);
                Cells[i]._name.color = col;
                Cells[i]._value.color = col;
                Cells[i]._value.text = GetValue(i, lerp * lerp * lerp);
            }
        }
        else
        {
            Cells[0]._value.text = GetValue(0, 1);
        }
    }
}
