using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject playerPrefab;
    public Vector2 playerPos;

    private GameObject camera;
    private bool gameStarted;
    private TimeManager timeManager;
    private Spawner[] spawners;
    private GameObject player;

    void Awake()
    {
        spawners = GameObject.FindObjectsOfType<Spawner>();
        timeManager = GetComponent<TimeManager>();
        camera = GameObject.Find("Camera");
    }
	// Use this for initialization
	void Start () {
        ResetGame();
	}
	
	// Update is called once per frame
	void Update () {
	    if(!gameStarted && Time.timeScale == 0)
        {
            if (Input.anyKeyDown)
            {
                timeManager.ManipulateTime(1, 1f);
                ResetGame();
            }
        }
	}

    void OnPlayerKilled()
    {
        foreach(Spawner spawn in spawners)
        {
            spawn.active = false;
        }

        var playerDestroyScript = player.GetComponent<DestroyOnEnemy>();
        playerDestroyScript.DestroyCallback -= OnPlayerKilled;
        timeManager.ManipulateTime(0, 5.5f);
        gameStarted = false;
    }

    void ResetGame()
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
        camera.GetComponent<CameraFollow>().LocatePlayer();
        gameStarted = true;
    }
}
