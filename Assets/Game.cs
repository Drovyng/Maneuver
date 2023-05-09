using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game instance;
    public static bool firstStarted;
    public static bool gameStarted;
    public static bool gameFailed;
    public static float gameCanStart;
    public static bool gameCanRestart;
    public static float timeScale = 1;
    private static int _score;
    public static int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            instance.scoreText.text = value.ToString();
        }
    }
    public Text scoreText;
    public Text downText;
    public GameObject obstaclePrefab;
    public GameObject obstaclesParent;

    public ParticleSystem particles;
    public SpriteRenderer scoreTenLine;

    public AudioClip[] sounds;
    public AudioSource soundPlayer;
    public static void GameStart()
    {
        gameStarted = true;
        instance.downText.color = Color.clear;
        instance.downText.text = "Click to restart";
    }
    public static void GameRestart()
    {
        gameStarted = false;
        gameFailed = false;
        gameCanRestart = false;
        gameCanStart = 0.1f;
        timeScale = 1;
        score = 0;
        
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public static void GameFail()
    {
        if (gameFailed) return;
        gameFailed = true;
        PlaySound(2);

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (_score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", _score);
        }
        else
        {
            instance.scoreText.text += "/" + bestScore;
        }
        timeScale = 0.2f;
        instance.particles.playbackSpeed = 0.2f;
        instance.StartCoroutine(instance.GameFailAnim());
    }
    public IEnumerator GameFailAnim()
    {
        yield return new WaitForSecondsRealtime(1f / 4f);
        Color color = Color.white;
        color.a = 0;
        while (color.a < 1)
        {
            yield return new WaitForSecondsRealtime(0.025f);
            color.a += 0.1f;
            downText.color = color;
        }
        gameCanRestart = true;
        yield break;
    }

    public Menu menu;
    public Player player;

    private void Start()
    {
        menu = FindAnyObjectByType<Menu>();
        menu.opened = true;
        menu.OpenClose();
        if (!firstStarted)
        {
            downText.text = "Click to start";
            firstStarted = true;
        }
        else
        {
            downText.color = Color.clear;
        }
        player = FindObjectOfType<Player>();
        instance = this;
        for (float i = 0; i < 25; i += 2.5f)
        {
            Instantiate(obstaclePrefab, new Vector3(0, 6 + i), Quaternion.identity, obstaclesParent.transform);
        }
        Menu.ApplyColorTheme();
    }
    private void FixedUpdate()
    {
        if (gameCanStart > 0)
        {
            gameCanStart = Mathf.Max(0, gameCanStart - Time.fixedDeltaTime);
        }

        if (player.transform.position.y > transform.position.y - 2.5f)
        {
            transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, player.transform.position.y + 2.5f, Time.fixedDeltaTime * 2.75f));
        }
        Physics2D.Simulate(Time.fixedDeltaTime * timeScale);
    }
    public void ScreenTouch()
    {
        if (menu.opened || gameCanStart > 0)
        {
            return;
        }
        if (!gameStarted)
        {
            GameStart();
            player.rigidbody.simulated = true;
            player.Jump();
        }
        else if (!gameFailed)
        {
            player.Jump();
        }
        else if (gameCanRestart)
        {
            GameRestart();
        }
    }
    public static void PlaySound(int index)
    {
        instance.soundPlayer.PlayOneShot(instance.sounds[index], instance.menu.soundVolume);
    }
}