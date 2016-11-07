using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject playerPrefab;
    public Vector2 playerPos;

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
	void Start () {
        ResetGame();
	}
	
    void OnPlayerKilled()
    {
        foreach(Spawner spawn in spawners)
        {
            spawn.active = false;
        }

        var playerDestroyScript = player.GetComponent<DestroyOnEnemy>();
        playerDestroyScript.DestroyCallback -= OnPlayerKilled;

        windowManager.Open(0);

        gameStarted = false;
    }

    public void ResetGame()
    {
        foreach(Spawner spawn in spawners)
        {
            spawn.active = true;
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            GameObjectUtil.Destroy(enemy);
        }
        player = GameObjectUtil.Instantiate(playerPrefab, new Vector3 (playerPos.x, playerPos.y, 0));

        var playerDestroyScript = player.GetComponent<DestroyOnEnemy>();
        playerDestroyScript.DestroyCallback += OnPlayerKilled;
        cam.GetComponent<CameraFollow>().LocatePlayer();
        if(windowManager.GetComponentInChildren<GameOverWindow>() != null)
        {
            windowManager.GetComponentInChildren<GameOverWindow>().Close();
        }

        gameStarted = true;
    }
}
