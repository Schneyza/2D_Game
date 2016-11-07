using UnityEngine;
using System.Collections;

public class DestroyOnProjectile : MonoBehaviour {

    private GameController gc;

    void Start()
    {
        gc = GameObject.FindObjectOfType<GameController>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            gc.invcrementKills();
            GameObjectUtil.Destroy(transform.gameObject);
        }
    }
}
