using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    public Animator animj;

    public Collider2D coll;

    public LayerMask ground;

    public float speed;
    public float jumpfore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        SwitchAnim();
    }

    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        if(horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed*Time.deltaTime, rb.velocity.y);
            animj.SetFloat("run",Mathf.Abs(facedirection));
        }
        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpfore * Time.deltaTime);
            animj.SetBool("jump", true);
        }
    }
    void SwitchAnim()
    {
        animj.SetBool("idle", false);

        if (animj.GetBool("jump"))
        {
            if(rb.velocity.y < 0)
            {
                animj.SetBool("jump", false);
                animj.SetBool("fall", true);
            }
        }else if (coll.IsTouchingLayers(ground))
        {
            animj.SetBool("fall", false);
            animj.SetBool("idle", true);
        }
    }
}
