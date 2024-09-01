using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu Instance { get; private set; }

    public static int OpenedPage = -1;
    [SerializeField] private GameObject _buttonGlow;
    [SerializeField] private ButtonPlus[] _pagesButtons;
    [SerializeField] private MenuPage[] _pages;
    private float _lerp = 0;

    public Text GameNameText;

    private void OnEnable()
    {
        _pagesButtons[0].OnClick.AddListener(() => OpenPage(0));
        _pagesButtons[1].OnClick.AddListener(() => OpenPage(1));
        _pagesButtons[2].OnClick.AddListener(() => OpenPage(2));
        _pagesButtons[3].OnClick.AddListener(() => OpenPage(3));
        _pagesButtons[4].OnClick.AddListener(() => OpenPage(4));
    }
    private void OnDisable()
    {
        for (int i = 0; i < 5; i++)
        {
            _pagesButtons[i].OnClick.RemoveAllListeners();
        }
    }
    public void SetTextColor(Color left, Color right)
    {
        GameNameText.color = left;
        GameNameText.text = "Mane<color='#"+ColorUtility.ToHtmlStringRGB(right)+"'>uver</color>";
    }

    private void Awake()
    {
        Instance = this;

        foreach (var page in _pages)
        {
            page.Create();
        }

        if (Game.alreadyStarted)
        {
            gameObject.SetActive(false);
            return;
        }
        var pg = OpenedPage;
        OpenedPage = -1;
        OpenPage(pg);
    }
    private List<float> scales = new() { 0.9f, 0.9f, 0.9f, 0.9f, 0.9f };
    private void OpenPage(int page)
    {
        if (page == -1) page = 2;
        if (page == 0 || page == 4) return;

        if (page == OpenedPage) 
            return;

        OpenedPage = page;
        _lerp = 0;

        for (int i = 0; i < 5; i++)
        {
            scales[i] = _pagesButtons[i].transform.localScale.x;
            _pages[i].gameObject.SetActive(i == page);
            if (i == page) _buttonGlow.transform.position = _pagesButtons[i].transform.position;
        }
        _pages[page].Open();
        _pages[page].Opening(0f);
    }
    private void FixedUpdate()
    {
        if (_lerp >= 1) return;
        _lerp += Time.fixedDeltaTime * 3;
        if (_lerp > 1) _lerp = 1;

        float lerp = 1f - MathF.Pow(1f - _lerp, 5f); // easeOutQuint

        for (int i = 0; i < 5; i++)
        {
            _pagesButtons[i].transform.localScale = Vector3.one * Mathf.Lerp(scales[i], i == OpenedPage ? 1.1f : 0.9f, lerp);
        }
        _buttonGlow.transform.localScale = Vector3.one * lerp;
    }
    public void Hide() => gameObject.SetActive(false);
}
