using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_opossum : Enemy
{
    // Start is called before the first frame update
    public Collider2D collider;
    public Rigidbody2D rb;
    public Transform leftPoint, rightPoint;
    public float speed;
    private bool faceLeft= true;
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
        if (faceLeft)
        {
            rb.velocity = new Vector2(-speed * Time.fixedDeltaTime, rb.position.y);

            if (transform.position.x < leftPoint.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }

        }
        else
        {

            rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.position.y);

            if (transform.position.x > rightPoint.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }

    }
}
