using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_egle : Enemy
{
    // Start is called before the first frame update
    public Collider2D collider;
    public Rigidbody2D rb;
    public Transform topPoint, bottomPoint;
    public float speed;

    public LayerMask ground;
    private bool toUp = true;
    protected override void Start()
    {
        base.Start();
        rb.GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }
    void SwitchAnimtor()
    {

    }
    void Movement()
    {
        if (toUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed * Time.fixedDeltaTime);
            if (transform.position.y > topPoint.position.y)
            {
                toUp = false;
            }

        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed * Time.fixedDeltaTime);
            if (transform.position.y < bottomPoint.position.y)
            {
                toUp = true;
            }
        }
    }

}
