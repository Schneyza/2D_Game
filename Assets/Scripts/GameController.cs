using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{

    public GameObject playerPrefab;
    public Vector2 playerPos;
    public Text survivedText;
    public Text killsText;

    private float timeSurvived;
    private int kills;
    private float score;
    private GameObject cam;
    private bool gameStarted;
    private Spawner[] spawners;
    private GameObject player;
    private WindowManager windowManager;

    void Awake()
    {
        GameObjectUtil.pools.Clear();
        spawners = GameObject.FindObjectsOfType<Spawner>();
        cam = GameObject.Find("Camera");
        windowManager = GameObject.FindObjectOfType<WindowManager>();
        windowManager.defaultWindowID = -1;
    }
    // Use this for initialization
    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (gameStarted && survivedText != null && killsText != null)
        {
            timeSurvived += Time.deltaTime;
            survivedText.text = "SURVIVED: " + formatTime(timeSurvived);
            killsText.text = "KILLS: " + kills.ToString();
        }
    }

    void OnPlayerKilled()
    {
        foreach (Spawner spawn in spawners)
        {
            spawn.active = false;
        }

        var playerDestroyScript = player.GetComponent<DestroyOnEnemy>();
        playerDestroyScript.DestroyCallback -= OnPlayerKilled;

        survivedText.gameObject.SetActive(false);
        killsText.gameObject.SetActive(false);
        score = (timeSurvived * kills);

        windowManager.Open(0);

        gameStarted = false;
    }

    public void ResetGame()
    {
        foreach (Spawner spawn in spawners)
        {
            spawn.active = true;
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            GameObjectUtil.Destroy(enemy);
        }
        player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(playerPos.x, playerPos.y, 0));

        var playerDestroyScript = player.GetComponent<DestroyOnEnemy>();
        playerDestroyScript.DestroyCallback += OnPlayerKilled;
        cam.GetComponent<CameraFollow>().LocatePlayer();

        survivedText.gameObject.SetActive(true);
        killsText.gameObject.SetActive(true);
        timeSurvived = 0f;
        kills = 0;

        if (windowManager.GetComponentInChildren<GameOverWindow>() != null)
        {
            windowManager.GetComponentInChildren<GameOverWindow>().Close();
        }

        gameStarted = true;
    }

    string formatTime(float value)
    {
        TimeSpan t = TimeSpan.FromSeconds(value);

        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    public void invcrementKills()
    {
        kills++;
    }

    public string getTime()
    {
        return formatTime(timeSurvived);
    }

    public string getKills()
    {
        return kills.ToString();
    }

    public string getScore()
    {
        return((int)score).ToString();
    }
}
