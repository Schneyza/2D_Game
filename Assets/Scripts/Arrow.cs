using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour, IRecycle {

    private Rigidbody2D body2d;
    private Shoot shoot;

    public float velX = 0f;
    public float velY = 0f;

    void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
        shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
    }

	// Use this for initialization
	void Start () {
        body2d.velocity = new Vector2(velX, velY);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag != "Player" && target.gameObject.tag != "Projectile")
        {
            GameObjectUtil.Destroy(transform.gameObject);
        }
    }

    public void Restart()
    {
        body2d.velocity = new Vector2(shoot.velX, shoot.velY);
        transform.rotation = Quaternion.Euler(0, 0, shoot.rotation);
    }

    public void Shutdown()
    {

    }
}
