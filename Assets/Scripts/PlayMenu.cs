using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class PlayMenu : MonoBehaviour
{
    public static PlayMenu Instance;
    public bool active;
    private bool _goActive;
    private float _lerp;

    [SerializeField]
    private Image _playPanels;
    [SerializeField]
    private Button _btnContinue;
    [SerializeField]
    private Button _btnToMenu;

    private void Continue()
    {
        SetActive(false);
    }
    private void ToMenu()
    {
        Game.alreadyStarted = false;
        Game.gamePrepared = false;
        Game.level = null;
        Game.GameRestart();
    }
    private void OnEnable()
    {
        _btnContinue.onClick.AddListener(Continue);
        _btnToMenu.onClick.AddListener(ToMenu);
    }
    private void OnDisable()
    {
        _btnContinue.onClick.RemoveAllListeners();
        _btnToMenu.onClick.RemoveAllListeners();
    }
    private void Start()    // I Can't use Awake because got error :|
    {
        Instance = this;
        _goActive = false;
        active = true;
        _lerp = 1;
        FixedUpdate();
    }
    private void FixedUpdate()
    {
        if (_goActive != active)
        {
            _lerp += Time.fixedDeltaTime * 3;

            if (_lerp >= 1)
            {
                _lerp = 1;
                active = _goActive;
                if (active)
                    Game.Instance.particles.Play();
                else
                    Game.Instance.particles.Pause();
            }
            float lerp = _goActive ? 1 - _lerp : _lerp;
            transform.localScale = Vector3.one * (1 - MathF.Pow(Mathf.Min(lerp * 1.25f, 1), 5f));   // easeOutQuint

            _playPanels.color = new Color(0, 0, 0, (1 - MathF.Pow(Mathf.Max(lerp * 1.5f - 0.5f, 0), 5f)) * 0.7f); // easeOutQuint

            _playPanels.raycastTarget = active || _goActive;
            Game.paused = active || _goActive;
        }
    }
    public void SetActive(bool active)
    {
        if (Game.gamePrepared && _goActive != active)
        {
            _goActive = active;
            this.active = !active;
            _lerp = 1 - _lerp;
        }
    }
}
