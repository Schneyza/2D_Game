using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smooth = 10.0f;
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(
                transform.position, new Vector3(target.position.x, target.position.y, -1),
                Time.deltaTime * smooth);
        }
    }
}
