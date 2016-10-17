using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public GameObject prefab;
    public float delay = 2.0f;
    public bool active = true;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(EnemyGenerator());
    }

    IEnumerator EnemyGenerator()
    {
        yield return new WaitForSeconds(delay);
        if (active)
        {
            var newTransform = transform;
            Instantiate(prefab, newTransform.position, Quaternion.identity);
        }
        StartCoroutine(EnemyGenerator());
    }
}
