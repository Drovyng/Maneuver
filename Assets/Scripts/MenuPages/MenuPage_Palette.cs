using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage_Palette : MenuPage
{
    public static Sprite RandomTexture;
    [SerializeField]
    private Sprite _randomTexture;
    [SerializeField]
    private GameObject _paletteCellPrefab;
    [SerializeField]
    private List<GameObject> _cells;
    [SerializeField]
    private RectTransform _contentRect;

    private float _lerp;
    public override void Create()
    {
        RandomTexture = _randomTexture;

        _cells = new List<GameObject>();
        for (int i = 0; i <= Utils.C_Colors.Count; i++)
        {
            var cell = Instantiate(_paletteCellPrefab, _contentRect);
            cell.GetComponent<MenuPage_Palette_Cell>().SetStyle(i - 1);
            cell.GetComponent<RectTransform>().anchoredPosition = new Vector2((i % 3 - 1)*460, i / 3 * -460 - 300);
            _cells.Add(cell);
        }
        _contentRect.sizeDelta = new Vector2(_contentRect.sizeDelta.x, 600 + _cells.Count / 3 * 460);
    }
    public override void Open()
    {
        _lerp = 0;
    }
    public override void Opening(float elapsed)
    {
        if (_lerp >= _cells.Count)
            return;

        _lerp += elapsed * 4f;
        if (_lerp > _cells.Count) _lerp = _cells.Count;


        for (int i = 0; i < _cells.Count; i++)
        {
            float i2 = i / 4f;
            float lerp = 1f - MathF.Pow(1f - MathF.Min(1, MathF.Max(0, _lerp - i2) / 4f), 5f); // easeOutQuint

            _cells[i].transform.localScale = Vector3.one * lerp;
        }

    }
}
