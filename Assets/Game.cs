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
        instance.StartCoroutine(instance.GameFailAnim());
    }
    public IEnumerator GameFailAnim()
    {
        yield return new WaitForSeconds(1f / 3f);
        Color color = Color.white;
        color.a = 0;
        while (color.a < 1)
        {
            yield return new WaitForFixedUpdate();
            color.a += Time.fixedDeltaTime * 3;
            downText.color = color;
        }
        gameCanRestart = true;
        yield break;
    }

    public Player player;

    private void Start()
    {
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
        if (player.transform.position.y > transform.position.y - 2.5f)
        {
            transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, player.transform.position.y + 2.5f, Time.fixedDeltaTime * 2f));
        }
        Physics2D.Simulate(Time.fixedDeltaTime * timeScale);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!gameStarted)
            {
                GameStart();
                player.rigidbody.simulated = true;
            }
            else if (gameFailed && gameCanRestart)
            {
                GameRestart();
            }
        }
    }
    public static void PlaySound(int index)
    {
        instance.soundPlayer.PlayOneShot(instance.sounds[index]);
    }
}