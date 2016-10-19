using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{

    public float shootDelay = 0.5f;
    public GameObject arrowPrefab;

    private Animator animator;
    private float timeElapsed = 0f;
    
    private float velX;
    private float velY;
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
                timeElapsed = 0;

                if (animState == 5 || animState == 1)
                {
                    rotation = 270;
                    velX = 100;
                    velY = 0;
                }
                if (animState == 6 || animState == 2)
                {
                    rotation = 90;
                    velX = -100;
                    velY = 0;
                }
                if (animState == 7 || animState == 3)
                {
                    rotation = 0;
                    velX = 0;
                    velY = 100;
                }
                if (animState == 0 || animState == 4)
                {
                    rotation = 180;
                    velX = 0;
                    velY = -100;
                }
                CreateArrow(transform.position);
            }

            timeElapsed += Time.deltaTime;
        }
    }

    public void CreateArrow(Vector2 pos)
    {
        var clone = GameObjectUtil.Instantiate(arrowPrefab, pos, Quaternion.Euler(0, 0, rotation));
        Arrow arrow = clone.GetComponent<Arrow>();
        arrow.velX = velX;
        arrow.velY = velY;
        clone.transform.localScale = transform.localScale;       
    }
}
