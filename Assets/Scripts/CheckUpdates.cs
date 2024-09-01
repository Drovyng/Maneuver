using System;
using System.Net.Http;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CheckUpdates : MonoBehaviour
{
    [SerializeField]
    private Image _playPanels;
    public static CheckUpdates Instance;
    static HttpClient httpClient = new HttpClient();
    public const int GameVersion = 2;
    private bool _yes = false;
    private float _lerp = 1;
    private static async void CheckUpdate()
    {
        try
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://raw.githubusercontent.com/Drovyng/Maneuver/main/GameVersion");
            var result = await httpClient.SendAsync(request);
            var text = await result.Content.ReadAsStringAsync();
            var version = int.Parse(text.Replace(" ", ""));
            if (version != GameVersion)
            {
                Instance._lerp = 0;
                PlayMenu.Instance.enabled = false;
            }
            Instance._yes = true;
        }
        finally { }
    }
    public static void OpenGamePage()
    {
        Application.OpenURL("https://www.rustore.ru/catalog/app/com.Drovyng.Maneuver");
        Application.Quit();
    }
    public void Close()
    {
        _playPanels.color = Color.clear;
        gameObject.SetActive(false);
        PlayMenu.Instance.enabled = true;
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        transform.localScale = Vector3.zero;
        if (!GameData.outdatedAlready)
        {
            var thrd = new Thread(CheckUpdate);
            thrd.Start();
        }
    }
    private void FixedUpdate()
    {
        if (_yes && !GameData.outdatedAlready)
        {
            GameData.outdatedAlready = true;
        }
        if (_lerp < 1)
        {
            _lerp += Time.fixedDeltaTime * 1.5f;
            if (_lerp > 1) _lerp = 1;

            transform.localScale = Vector3.one * (1 - MathF.Pow(1 - _lerp, 5f));   // easeOutQuint
            _playPanels.color = new Color(0, 0, 0, (1 - MathF.Pow(1 - _lerp, 5f)) * 0.7f); // easeOutQuint
        }
    }
}
