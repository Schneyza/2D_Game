using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public float smooth = 10.0f;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(
                transform.position, new Vector3(target.position.x, target.position.y, transform.position.z),
                Time.deltaTime * smooth);
        }
    }
}
