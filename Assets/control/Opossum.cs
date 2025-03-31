using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Enemy
{
    private Rigidbody2D rb;
    private Animation anim;
    public Transform leftpoint,rightpoint;
    private float leftx,rightx;
    public float speed;

    private bool faceleft = true;

    protected override void Start()
    {
        base.Start();
        anim= GetComponent<Animation>();
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(faceleft)
        {
            rb.velocity = new Vector2(-speed, 0);
            if(transform.position.x<leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceleft = false;
            }
        }
        else
        {
            rb.velocity=new Vector2(speed, 0);
            if(transform.position.x>rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
            }
        }
    }
}
