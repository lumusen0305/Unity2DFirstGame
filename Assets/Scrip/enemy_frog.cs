using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_frog : Enemy
{
    // private Animator animator;
    public Collider2D collider;
    public Rigidbody2D rb;
    public Transform leftPoint, rightPoint;
    public float speed;
    public float jumpFroce;
    private bool faceLeft;
    public LayerMask ground;

    // Start is called before the first frame update
   protected override void Start()
    {
         base.Start ();
        faceLeft = true;
        rb.GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SwitchAnimtor();
    }

    void Movement()
    {
        if (faceLeft)
        {
            if (collider.IsTouchingLayers(ground))
            {
                animator.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed * Time.deltaTime, jumpFroce * Time.deltaTime);
            }
            if (transform.position.x < leftPoint.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }

        }
        else
        {
            if (collider.IsTouchingLayers(ground))
            {
                animator.SetBool("jumping", true);
                rb.velocity = new Vector2(speed * Time.deltaTime, jumpFroce * Time.deltaTime);
            }
            if (transform.position.x > rightPoint.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }

    }
    void SwitchAnimtor()
    {
        if (animator.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
            }
        }
        else if (animator.GetBool("falling") && collider.IsTouchingLayers(ground))
        {
            animator.SetBool("falling", false);

        }
    }

}
