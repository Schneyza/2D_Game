using UnityEngine;
using System.Collections;

public class DestroyOnProjectile : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            GameObjectUtil.Destroy(transform.gameObject);
        }
    }
}
