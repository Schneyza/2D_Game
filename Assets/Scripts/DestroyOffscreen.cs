using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Mathf.Abs(transform.position.x) > 1000 || Mathf.Abs(transform.position.y) > 1000)
        {
            GameObjectUtil.Destroy(transform.gameObject);
        }
	}
}
