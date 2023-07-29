using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{
    private Rigidbody2D rb;
    
    private Collider2D coll;

    public Transform top, bottom; 

    public float speed;
    private float topY, bottomY;

    private bool isUp;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();

        coll = GetComponent<Collider2D>();

        topY = top.position.y;
        bottomY = bottom.position.y;

        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if(transform.position.y > topY)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if(transform.position.y < bottomY)
            {
                isUp = true;
            }
        }
    }
}
