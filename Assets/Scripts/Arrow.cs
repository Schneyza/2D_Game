using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    private Rigidbody2D body2d;

    public float velX = 0f;
    public float velY = 0f;

    void Awake()
    {
        body2d = GetComponent<Rigidbody2D>(); 
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
            Destroy(transform.gameObject);
        }
    }
}
