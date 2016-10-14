using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public float Speed = 1;
    public float FastSpeed = 3;
    public KeyCode EnableFastSpeedWithKey = KeyCode.LeftShift;
    public float runMultiplier = 2f;
    public bool running;

    private Rigidbody2D body2d;

    private Animator animator;

    void Awake()
    {
        body2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var currentSpeed = Speed;
        if (Input.GetKey(EnableFastSpeedWithKey))
        {
            currentSpeed = FastSpeed;
            running = true;
        } else
        {
            running = false;
        }
        var movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        transform.Translate(movement * currentSpeed * Time.deltaTime);

        animator.speed = running ? runMultiplier : 1;
        
        if (Input.GetButton("Right"))
        {
            ChangeAnimationState(1);
        }
        else if (Input.GetButtonUp("Right"))
        {
            ChangeAnimationState(5);
        }
        if (Input.GetButton("Left"))
        {
            ChangeAnimationState(2);
        }
        else if (Input.GetButtonUp("Left"))
        {
            ChangeAnimationState(6);
        }
        if (Input.GetButton("Up"))
        {
            ChangeAnimationState(3);
        }
        else if (Input.GetButtonUp("Up"))
        {
            ChangeAnimationState(7);
        }
        if (Input.GetButton("Down"))
        {
            ChangeAnimationState(4);
        }
        else if (Input.GetButtonUp("Down"))
        {
            ChangeAnimationState(0);
        }



    }

    void ChangeAnimationState(int value)
    {
        animator.SetInteger("AnimState", value);
    }


}
