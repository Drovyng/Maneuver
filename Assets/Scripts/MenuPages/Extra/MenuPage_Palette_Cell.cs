using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonPlus))]
public class MenuPage_Palette_Cell : MonoBehaviour
{
    [SerializeField] private Image _obstacle;
    [SerializeField] private Image _player;
    [SerializeField] private ButtonPlus _button;
    [SerializeField] private Text _name;
    private int color;
    private void OnClick()
    {
        if (GameData.Instance.color != color)
        {
            GameData.Instance.color = color;
            GameData.Save();
            Game.Instance.ApplyColors();
        }
    }
    private void Awake()
    {
        _button = GetComponent<ButtonPlus>();
    }

    private void OnEnable() => _button.OnClick.AddListener(OnClick);
    private void OnDisable() => _button.OnClick.RemoveAllListeners();

    public void SetStyle(int color)
    {
        this.color = color;
        if (color < 0)
        {
            _name.text = "random";
            _obstacle.color = Color.clear;
            _player.color = Color.white;
            _player.sprite = MenuPage_Palette.RandomTexture;
            return;
        }
        _obstacle.color = Utils.C_Colors[color].obstacle;
        _player.color = Utils.C_Colors[color].player + Color.white * 0.25f;
        _name.text = Utils.C_Colors[color].name;
    }
}
