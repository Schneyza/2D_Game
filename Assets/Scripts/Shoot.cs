using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{

    public float shootDelay = 0.5f;
    public Arrow arrowPrefab;

    private Animator animator;
    private float timeElapsed = 0f;

    private float rotation;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (arrowPrefab != null)
        {
            var animState = animator.GetInteger("AnimState");
            var canFire = Input.GetButton("Fire1");

            if (canFire && timeElapsed > shootDelay)
            {
                CreateArrow(transform.position);
                timeElapsed = 0;

                if (animState == 5 || animState == 1)
                {
                    rotation = 270;
                    arrowPrefab.velX = 100;
                    arrowPrefab.velY = 0;
                }
                if (animState == 6 || animState == 2)
                {
                    rotation = 90;
                    arrowPrefab.velX = -100;
                    arrowPrefab.velY = 0;
                }
                if (animState == 7 || animState == 3)
                {
                    rotation = 0;
                    arrowPrefab.velX = 0;
                    arrowPrefab.velY = 100;
                }
                if (animState == 0 || animState == 4)
                {

                    Debug.Log("HERE");
                    rotation = 180;
                    arrowPrefab.velX = 0;
                    arrowPrefab.velY = -100;
                }
            }

            timeElapsed += Time.deltaTime;
        }
    }

    public void CreateArrow(Vector2 pos)
    {

        var clone = Instantiate(arrowPrefab, pos, transform.rotation) as Arrow;
        clone.transform.localScale = transform.localScale;
        clone.transform.rotation = Quaternion.Euler(0, 0, rotation);        
    }
}
