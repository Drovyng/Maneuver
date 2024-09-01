using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonPlus))]
public class MenuPage_Levels_Cell : MonoBehaviour
{
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text _time;
    [SerializeField]
    private List<Image> _stars;
    [SerializeField]
    private Image _bg;
    private string _id;
    public static readonly List<Color> Difficulties = new()
    {
        Color.HSVToRGB(0.3f, 0.7f, 0.9f),   // Easy
        Color.HSVToRGB(0.05f, 0.7f, 0.9f),  // Normal
        Color.HSVToRGB(0f, 0.7f, 0.9f),     // Hard
        Color.HSVToRGB(0.77f, 0.7f, 0.9f),  // Insane
    };
    public void SetData(string id, string name, int difficulty)
    {
        _id = id;
        _name.text = name;
        _bg.color = Difficulties[difficulty];
        _time.text = "-";
        for (int i = 0; i < 3; i++)
        {
            _stars[i].gameObject.SetActive(false);
        }
    }
    public void SetData(string id, string name, int difficulty, float time, int stars)
    {
        _id = id;
        _name.text = name;
        _time.text = Utils.TimeToString(time);
        _bg.color = Difficulties[difficulty];
        for (int i = 0; i < 3; i++)
        {
            _stars[i].gameObject.SetActive(true);
            _stars[i].color = i < stars ? Color.white : Color.black;
        }
    }
    private void OnClick()
    {
        Menu.Instance.Hide();
        Game.level = _id;
        Game.GamePrepare();
    }
    [SerializeField] private ButtonPlus _button;
    private void Awake()
    {
        _button = GetComponent<ButtonPlus>();
    }

    private void OnEnable() => _button.OnClick.AddListener(OnClick);
    private void OnDisable() => _button.OnClick.RemoveAllListeners();
}
