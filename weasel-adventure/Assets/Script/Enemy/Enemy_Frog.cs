using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rb;

    //private Animator anim;

    private Collider2D Coll;

    public LayerMask ground;

    public Transform leftPoint, rightPoint;
    
    public float Speed, jumpForce;
    private float leftX, rightX;
    
    private bool faceLeft = true;
    private bool isMoving = true;

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
    
        //anim = GetComponent<Animator>();

        Coll = GetComponent<Collider2D>();

        transform.DetachChildren();

        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;

        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {
        if (isMoving)
        {
            if (faceLeft)
            {
                if (Coll.IsTouchingLayers(ground))
                {
                    anim.SetBool("frog_jump", true);
                    rb.velocity = new Vector2(-Speed, jumpForce);
                }
                if (transform.position.x < leftX)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    faceLeft = false;
                }
            }
            else
            {
                if (Coll.IsTouchingLayers(ground))
                {
                    anim.SetBool("frog_jump", true);
                    rb.velocity = new Vector2(Speed, jumpForce);
                }
                if (transform.position.x > rightX)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    faceLeft = true;
                }
            }
        }
    }

    void SwitchAnim()
    {
        if (anim.GetBool("frog_jump"))
        {
            if(rb.velocity.y < 0.1)
            {
                anim.SetBool("frog_jump", false);
                anim.SetBool("frog_fall", true);
            }
        }
        if (Coll.IsTouchingLayers(ground) && anim.GetBool("frog_fall"))
        {
            anim.SetBool("frog_fall", false);
        }
    }
}
