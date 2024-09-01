using System.Collections.Generic;
using UnityEngine;

public class MenuPage_Levels : MenuPage
{
    [SerializeField]
    private GameObject _levelCellPrefab;
    [SerializeField]
    private RectTransform _contentRect;

    private List<MenuPage_Levels_Cell> _cells = new();
    private float _lerp;

    public override void Create()
    {
        for (int i = 0; i < Level.Levels.Count; i++)
        {
            var cell = Instantiate(_levelCellPrefab, _contentRect);
            var level = Level.Levels[i];
            var component = cell.GetComponent<MenuPage_Levels_Cell>();
            _cells.Add(component);
            var index = GameData.Instance.levels.FindIndex((lol) => lol.id == level.id);
            var time = index != -1 ? GameData.Instance.levels[index].time : -1;
            if (time > 0)
            {
                component.SetData(level.id, level.GetName(), 0, time, index != -1 ? Level.GetStarsByTime(level.id, time) : 0);
            }
            else
            {
                component.SetData(level.id, level.GetName(), 0);
            }
            cell.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * 350 - 200);
        }
        _contentRect.sizeDelta = new Vector2(_contentRect.sizeDelta.x, 200 + Level.Levels.Count * 350);
    }
    public override void Open()
    {
        for (int i = 0; i < Level.Levels.Count; i++)
        {
            var level = Level.Levels[i];
            var index = GameData.Instance.levels.FindIndex((lol) => lol.id == level.id);
            var time = index != -1 ? GameData.Instance.levels[index].time : -1;
            if (time > 0)
            {
                _cells[i].SetData(level.id, level.GetName(), 0, time, index != -1 ?  Level.GetStarsByTime(level.id, time) : 0);
            }
            else
            {
                _cells[i].SetData(level.id, level.GetName(), 0);
            }
        }
        _lerp = 0;
    }
    public override void Opening(float elapsed)
    {

    }
}
