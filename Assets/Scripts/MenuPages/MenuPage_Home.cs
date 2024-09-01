using System;
using Unity.VisualScripting;
using UnityEngine;

public class MenuPage_Home : MenuPage
{
    [SerializeField] private ButtonPlus _playButton;

    private float _lerp;
    public override void Create()
    {
    }
    public override void Open()
    {
        _lerp = 0;
        _playButton.transform.localScale = Vector3.zero;
    }
    public override void Opening(float elapsed)
    {
        if (_lerp >= 1) 
            return;

        _lerp += elapsed;
        if (_lerp > 1) _lerp = 1;

        float lerp = 1f - MathF.Pow(1f - _lerp, 5f); // easeOutQuint

        _playButton.transform.localScale = Vector3.one * lerp;
    }
    private void ButtonPlay()
    {
        Menu.Instance.Hide();
        Game.GamePrepare();
    }
    private void OnEnable()
    {
        _playButton.OnClick.AddListener(ButtonPlay);
    }
    private void OnDisable()
    {
        _playButton.OnClick.RemoveAllListeners();
    }
}
