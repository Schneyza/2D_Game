using UnityEngine;
using System.Collections;

public class DestroyOnEnemy : MonoBehaviour {

    public delegate void OnDestroy();
    public event OnDestroy DestroyCallback;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            GameObjectUtil.Destroy(transform.gameObject);

            if (DestroyCallback != null)
            {
                DestroyCallback();
            }
        }
    }
}
