using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    private Rigidbody2D rb;
    private Animator anim;
    public Transform uppoint, downpoint;
    public float speed;
    private float upy, downy;

    private bool flydown = true;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        upy = uppoint.position.y;
        downy = downpoint.position.y;
        Destroy(uppoint.gameObject);
        Destroy(downpoint.gameObject);
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (flydown)
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < downy)
            {
                flydown = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y > upy)
            {
                flydown = true;
            }
        }
    }
}
