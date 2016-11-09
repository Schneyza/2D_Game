using UnityEngine;
using System.Collections;

public class DestroyOnProjectile : MonoBehaviour {

    public delegate void OnEnemyKill();
    public static event OnEnemyKill EnemyKillCallback;
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            GameObjectUtil.Destroy(transform.gameObject);
            if(EnemyKillCallback != null)
            {
                EnemyKillCallback();
            }
        }
    }
}