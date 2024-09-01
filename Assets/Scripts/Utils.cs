using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public static class Utils
{
    public struct Colors
    {
        public string name;
        public Color player;
        public Color obstacle;

        public Colors(string name, int player, int obstacle)
        {
            this.name = name;
            this.player = ColorPlayerP(player);
            this.obstacle = ColorObstacleP(obstacle);
        }
        public Colors(string name, Color player, Color obstacle)
        {
            this.name = name;
            this.player = player;
            this.obstacle = obstacle;
        }
    }
    /// <summary>
    /// Name, Player, Obstacle
    /// </summary>]
    public static readonly List<Colors> C_Colors = new()
    {
        new Colors("classic", 220, 0),
        new Colors("yellow & green", 120, 55),
        new Colors("pinkymania", 330, 180),
        new Colors("purple & red", 0, 280),
        new Colors("deuteranopia", 60, 120),
        new Colors("tritanopia", 210, 330),
        new Colors("monochrome", new Color(0.75f, 0.75f, 0.75f, 1f), Color.gray),
        new Colors("lime & blue", 80, 240),
        new Colors("hot", 0, 25)
    };
    private static int _curColor;
    public static int CurColor
    {
        get => _curColor;
        set {
            if (value < 0)
            {
                _curColor = Random.Range(0, C_Colors.Count);
                return;
            }
            _curColor = value;
        }
    }

    public static Color ColorPlayer => C_Colors[_curColor].player;
    public static Color ColorObstacle => C_Colors[_curColor].obstacle;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color ColorPlayerP(float hue) => ColorS(hue, 0.5f);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color ColorObstacleP(float hue) => ColorS(hue, 0.875f);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Color ColorS(float hue, float s) => Color.HSVToRGB(hue / 360f, s, 1f);

    public static string TimeToString(float time, int size = 50)
    {
        return (int)time + ".<size=" + size + ">" + ("" + (int)(time * 1000 % 1000 + 1000)).Substring(1) + "</size>";
    }


    /*

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Color ColorPlayer(float hue) => Color.HSVToRGB(hue / 360f, 0.5f, 1f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Color ColorObstacle(float hue) => Color.HSVToRGB(hue / 360f, 0.875f, 1f);
    */
}
