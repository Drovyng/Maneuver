using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public static bool alreadyStarted;
    public static bool gameStarted;
    public static bool gameFailed;
    public static float gameCanStart;
    public static bool gameCanRestart;
    public static bool gamePrepared;
    public static float timeScale = 1;
    private static int _score;
    public static bool paused;
    public static string level;
    public static float time;
    public static int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            Instance.scoreText.text = value.ToString();
        }
    }
    public Text scoreText;
    public Text downText;

    public ParticleSystem particles;
    [SerializeField]
    private SpriteRenderer _lastScoreLine;
    [SerializeField]
    private SpriteRenderer _maxScoreLine;

    public AudioClip[] sounds;
    public AudioSource soundPlayer;

    public SpriteRenderer[] bounds;
    public SpriteRenderer finish;

    public static bool gameWin;
    public static float levelTime1;
    public static float levelTime2;
    public bool notFollowPlayer;

    public static void GamePrepare()
    {
        if (level != null) Instance.StartLevel();
        else ObstacleSpawner.SpawnDefault();

        Instance.scoreText.enabled = true;
        Instance.downText.enabled = true;

        Instance.downText.enabled = true;
        gamePrepared = true;
        alreadyStarted = true;
    }
    public static void GameStart()
    {
        gameStarted = true;
        Instance.downText.color = Color.clear;
        Instance.downText.text = "Tap to restart";
        Instance.particles.Play();
    }
    public static void GameRestart()
    {
        gameStarted = false;
        gameFailed = false;
        gameCanRestart = false;
        gameCanStart = 0.1f;
        timeScale = 1;
        score = 0;

        if (level != null)
        {
            var index = GameData.Instance.levels.FindIndex((lol) => lol.id == level);
            var data = index != -1 ? GameData.Instance.levels[index] : new(level, 0, 0);
            if (time < data.time && gameWin)
            {
                data.time = time;
            }
            data.attempts++;
            if (index != -1)
                GameData.Instance.levels[index] = data;
            else
                GameData.Instance.levels.Add(data);

        }
        if (gameWin)
        {
            level = null;
            gamePrepared = false;
            alreadyStarted = false;
        }
        time = 0;

        gameWin = false;


        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public static void GameFail()
    {
        if (gameFailed) return;
        gameFailed = true;
        PlaySound(2);

        int bestScore = GameData.Instance.maxScore;
        GameData.Instance.totalScore += _score;
        GameData.Instance.lastScore = _score;
        GameData.Instance.attempts++;
        GameData.Instance.totalAttempts++;
        if (level == null)
        {
            if (_score > bestScore)
            {
                GameData.Instance.maxScore = _score;
                GameData.Save();
            }
            else
            {
                Instance.scoreText.text += "/" + bestScore;
            }
        }
        else
        {
            if (time <= levelTime1)
            {
                Instance.scoreText.text += "/" + Utils.TimeToString(levelTime1);
            }
            else if (time <= levelTime2)
            {
                Instance.scoreText.text += "/" + Utils.TimeToString(levelTime2);
            }
        }
        
        timeScale = 0.2f;
        Instance.particles.playbackSpeed = 0.2f;

        Instance.StartCoroutine(Instance.GameFailAnim());
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
    public void ApplyColors()
    {
        Utils.CurColor = GameData.Instance.color;

        player.Recolor();
        foreach (var bnd in bounds)
        {
            bnd.color = Utils.ColorObstacle;
        }
        var color = Utils.ColorPlayer + Color.white * 0.25f;
        color.a = 0.8f;
        _lastScoreLine.color = color;
        _maxScoreLine.color = color;

        var clr = Utils.ColorPlayer;
        clr.a = 0.5f;
        finish.color = clr;

        Menu.Instance.SetTextColor(Utils.ColorObstacle, Utils.ColorPlayer);
    }

    public Player player;
    private void Awake()
    {
        Instance = this;
    }
    private void StartLevel()
    {
        scoreText.fontSize = 140;
        scoreText.text = Utils.TimeToString(0, 60);
        var lv = Level.Levels.Find((lol) => lol.id == level);
        foreach (var obstacle in lv.obstacles)
        {
            ObstacleSpawner.Spawn(obstacle.Item1, obstacle.Item2);
        }
        player.leftRight = lv.playerLR;
        Level.LevelFeathuresStart(level);
        finish.size = new Vector2(lv.finish.z, lv.finish.w);
        finish.transform.position = new Vector3(lv.finish.x, lv.finish.y, 1);
        levelTime1 = lv.bestTime;
        levelTime2 = lv.mediumTime;
    }
    public static void LevelComplete()
    {
        alreadyStarted = false;
        gameWin = true;
        Instance.downText.color = Color.white;
        Instance.downText.text = "You Win!";
        gameFailed = true;                      // LOL
        gameCanRestart = true;
        GameData.Instance.totalAttempts++;
        PlaySound(3);
        timeScale = 0.2f;
        Instance.particles.playbackSpeed = 0.2f;
    }
    private void Start()
    {
        GameData.Save();

        player = FindObjectOfType<Player>();

        ApplyColors();

        if (alreadyStarted)
        {
            if (level == null) ObstacleSpawner.SpawnDefault();
            downText.text = "";
            Instance.scoreText.enabled = true;
            Instance.downText.enabled = true;
        }
        else
        {
            downText.text = "Tap to start";
        }
        if (level == null)
        {
            if (GameData.Instance.lastScore > 0)
            {
                _lastScoreLine.transform.position = new Vector3(0, 4.75f + 2.5f * GameData.Instance.lastScore, 0);
            }
            else
            {
                _lastScoreLine.enabled = false;
            }
            if (GameData.Instance.maxScore > GameData.Instance.lastScore)
            {
                _maxScoreLine.transform.position = new Vector3(0, 4.75f + 2.5f * GameData.Instance.maxScore, 0);
            }
            else
            {
                _maxScoreLine.enabled = false;
            }
        }
        else
        {
            StartLevel();
            _lastScoreLine.enabled = false;
            _maxScoreLine.enabled = false;
        }
    }
    private void Update()
    {
        GameData.Instance.totalTime += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (paused)
        {
            return;
        }
        if (gameCanStart > 0)
        {
            gameCanStart = Mathf.Max(0, gameCanStart - Time.fixedDeltaTime);
        }

        if (!notFollowPlayer && player.transform.position.y > transform.position.y - 2.5f)
        {
            transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, player.transform.position.y + 2.5f, Time.fixedDeltaTime * 2.75f));
        }

        if (level != null && gameStarted && !gameFailed)
        {
            time += Time.fixedDeltaTime;
            Level.LevelFeathuresUpdate(level);
            scoreText.text = Utils.TimeToString(time, 60);
            
        }

        Physics2D.Simulate(Time.fixedDeltaTime * timeScale);
    }
    public void ScreenTouch()
    {
        if (!gamePrepared) return;

        if (gameCanStart > 0)
        {
            return;
        }
        if (!gameStarted)
        {
            GameStart();
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
        Instance.soundPlayer.PlayOneShot(Instance.sounds[index], 1);
    }
}
