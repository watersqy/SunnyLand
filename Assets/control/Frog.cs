using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    public LayerMask ground;
    public Transform leftpoint, rightpoint;
    public float speed,jumpforce;
    private float leftx, rightx; 

    private bool faceleft = true;
    


    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
        coll=GetComponent<Collider2D>();
    }

    
    void Update()
    {
        Movement();
        Switchanim();
    }

    //ÒÆ¶¯
    void Movement()
    {
        if(faceleft)
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpforce);
            }
            if (transform.position.x< leftx)
            {
                
                transform.localScale = new Vector3(-1, 1, 1);
                faceleft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpforce);
            }
            if (transform.position.x > rightx)
            {
                
                transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
            }
        }
    }

    void Switchanim()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }

    
}
