using UnityEngine;

public class Enemy_Frog : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public Transform leftPoint, rightPoint;
    public float Speed;
    private float leftX, rightX;
    private bool faceLeft = true;

    private bool isMoving = true;
    private float stopTimer = 2f;
    private float stopTimeCounter = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        transform.DetachChildren();

        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isMoving)
        {
            if (faceLeft)
            {
                anim.SetBool("frog_jump", true);
                anim.SetBool("frog_idle", false);
                rb.velocity = new Vector2(-Speed, rb.velocity.y);
                if (transform.position.x < leftX)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    faceLeft = false;
                }
            }
            else
            {
                anim.SetBool("frog_jump", true);
                anim.SetBool("frog_idle", false);
                rb.velocity = new Vector2(Speed, rb.velocity.y);
                if (transform.position.x > rightX)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    faceLeft = true;
                }
            }

            stopTimeCounter += Time.deltaTime;
            if (stopTimeCounter >= stopTimer)
            {
                isMoving = false;
                stopTimeCounter = 0f;
                rb.velocity = Vector2.zero;
                anim.SetBool("frog_idle", true);
                anim.SetBool("frog_jump", false);
                Invoke("StartMoving", 2f);
            }
        }
    }

    void StartMoving()
    {
        isMoving = true;
    }
}
