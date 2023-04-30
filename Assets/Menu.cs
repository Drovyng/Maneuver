using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static (Color, Color)?[] colorThemes = new (Color, Color)?[]
    {
        null,
        (new Color(0.5f, 0.6666667f, 1, 1), new Color(0.85f, 0, 0, 1)),
        (new Color(1f, 0.7f, 0.5f, 1), new Color(0, 0.85f, 0.4f, 1)),
        (new Color(1f, 0.5f, 0.5f, 1), new Color(0.7f, 0, 0.844f, 1)),
        (new Color(0.5f, 1f, 0.55f, 1), new Color(0f, 0.56f, 0.85f, 1)),
        (new Color(0.58f, 0.5f, 1f, 1), new Color(0.85f, 0.78f, 0f, 1))
    };
    public static void ChangeColorTheme()
    {
        PlayerPrefs.SetInt("ColorTheme", (PlayerPrefs.GetInt("ColorTheme", 0) + 1) % colorThemes.Length);
        ApplyColorTheme();
    }
    public static void ApplyColorTheme()
    {
        int colorTheme = PlayerPrefs.GetInt("ColorTheme", 0);
        bool random = colorTheme == 0;
        if (random)
        {
            colorTheme = Random.Range(1, colorThemes.Length);
        }
        var colors = colorThemes[colorTheme].Value;
        if (random)
        {
            Game.instance.menu.colorDownText.text = "Randomized";
        }
        else
        {
            Game.instance.menu.colorDownText.text = "<color=\"#" + ColorUtility.ToHtmlStringRGB(colors.Item1) + "\">Player</color> <color=\"#" + UnityEngine.ColorUtility.ToHtmlStringRGB(colors.Item2) + "\">Obstacle</color>";
        }
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
    public bool opened;

    public float soundVolume;
    public bool soundVolumeMuted;


    public Image image;
    public void OpenClose()
    {
        opened = !opened;
        foreach (var obj in GetComponentsInChildren<Image>(true))
        {
            if (obj.gameObject != gameObject)
            {
                obj.gameObject.active = opened;
            }
        }
        image.enabled = opened;
    }
    public Image soundButton;
    public Sprite unmuted;
    public Sprite muted;
    public Slider soundSlide;
    public void Sound()
    {
        soundVolumeMuted = !soundVolumeMuted;
        soundButton.sprite = soundVolumeMuted ? muted : unmuted;
        PlayerPrefs.SetInt("SoundMuted", soundVolumeMuted ? 1 : 0);
        soundVolume = soundVolumeMuted ? 0 : soundSlide.value;
    }
    public void SoundSliderChanged()
    {
        soundVolume = soundVolumeMuted ? 0 : soundSlide.value;
        PlayerPrefs.SetFloat("SoundVolume", soundSlide.value);
    }
    public Text colorDownText;
    private void Start()
    {
        soundSlide.value = PlayerPrefs.GetFloat("SoundVolume", 1);
        SoundSliderChanged();
        if (PlayerPrefs.GetInt("SoundMuted", 0) == 1) Sound();
    }
}
