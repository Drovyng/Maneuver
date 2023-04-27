using UnityEngine;

public class Menu : MonoBehaviour
{
    public static (Color, Color)?[] colorThemes = new (Color, Color)?[]
    {
        null,
        (new Color(0.5f, 0.6666667f, 1, 1), new Color(0.8431373f, 0, 0, 1)),
        (new Color(1f, 0.7509804f, 0.5019608f, 1), new Color(0, 0.8431373f, 0.1f, 1))
    };
    public static void ApplyColorTheme()
    {
        int colorTheme = PlayerPrefs.GetInt("ColorTheme", 0);
        if (colorTheme == 0)
        {
            colorTheme = Random.Range(1, colorThemes.Length);
        }
        var colors = colorThemes[colorTheme].Value;
        foreach (var obj in FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            if (obj.gameObject.tag == "Player")
            {
                obj.GetComponent<SpriteRenderer>().color = colors.Item1;
            }
            else if (obj.gameObject.tag == "Obstacle")
            {
                obj.GetComponent<SpriteRenderer>().color = colors.Item2;
            }
        }
    }
}
