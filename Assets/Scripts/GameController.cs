using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public Vector2 playerPos;

    private GameObject cam;
    private Spawner[] spawners;
    private GameObject player;
    private WindowManager windowManager;

    public delegate void OnReset();
    public static event OnReset ResetCallback;
    public delegate void OnGameEnd();
    public static event OnGameEnd GameEndCallback;

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

    void OnPlayerKilled()
    {
        if (GameEndCallback != null)
        {
            GameEndCallback();
        }
        ActivateSpawners(false);

        //unregister OnPlayerKilled from DestroyCallback to avoid memory leaks
        var playerDestroyScript = player.GetComponent<DestroyOnEnemy>();
        playerDestroyScript.DestroyCallback -= OnPlayerKilled;

        windowManager.Open(0);
    }

    public void ResetGame()
    {
        if (ResetCallback != null)
        {
            ResetCallback();
        }
        ActivateSpawners(true);
        RemoveEnemies();
        ResetPlayer();

        if (windowManager.GetComponentInChildren<GameOverWindow>() != null)
        {
            windowManager.GetComponentInChildren<GameOverWindow>().Close();
        }
    }

    void ActivateSpawners(bool value)
    {
        foreach (Spawner spawn in spawners)
        {
            spawn.active = true;
        }
    }

    void RemoveEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            GameObjectUtil.Destroy(enemy);
        }
    }

    void ResetPlayer()
    {
        player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(playerPos.x, playerPos.y, 0));

        //Register OnPlayerKilled to execute when DestroyCallback in DestroyOnEnemy script is called
        var playerDestroyScript = player.GetComponent<DestroyOnEnemy>();
        playerDestroyScript.DestroyCallback += OnPlayerKilled;
        cam.GetComponent<CameraFollow>().LocatePlayer();
    }


}
