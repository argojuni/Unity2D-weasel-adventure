using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator animj;

    public Collider2D coll;

    public LayerMask ground;

    public GameObject explosionEffect; // Prefab efek ledakan

    public GameObject enemyDeath; // Prefab efek ledakan

    public Text cherryText;

    public float speed;
    public float jumpfore;

    public int cherry;

    private bool isHurt;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        animj = GetComponent<Animator>();

        cherryText.text = "Cherry: " + cherry.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }

        SwitchAnim();
    }

    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        if(horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed*Time.deltaTime, rb.velocity.y);
            animj.SetFloat("runing",Mathf.Abs(facedirection));
        }
        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        if (Input.GetButtonDown("Jump")&&coll.IsTouchingLayers(ground))
        {
            if(horizontalMove != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed * Time.deltaTime);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpfore * Time.deltaTime);
            }
            animj.SetBool("jumping", true);
        }
    }
    void SwitchAnim()
    {
        animj.SetBool("idle", false);

        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            animj.SetBool("falling", true);
        }

        if (animj.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            {
                animj.SetBool("jumping", false);
                animj.SetBool("falling", true);
            }
        }else if (isHurt)
        {
            animj.SetBool("hurt",true);
            animj.SetFloat("running", 0);

            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                animj.SetBool("hurt",false);
                animj.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {   
            animj.SetBool("falling", false);
            animj.SetBool("idle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Collection")
        {
            GameObject eksplosion = Instantiate(explosionEffect, coll.transform.position, Quaternion.identity); // Membuat efek ledakan
            
            Destroy(eksplosion, 0.5f);
            Destroy(col.gameObject);
            
            cherry += 1;
            
            cherryText.text = "Cherry: " + cherry.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
       if(collision.gameObject.tag == "Enemy")
       {
            if (animj.GetBool("falling"))
            {
                GameObject enemyDeatheffect = Instantiate(enemyDeath, coll.transform.position, Quaternion.identity); // Membuat efek ledakan

                Destroy(enemyDeatheffect, 0.5f);

                Destroy(collision.gameObject);

                rb.velocity = new Vector2(rb.velocity.x, jumpfore * Time.deltaTime);
                animj.SetBool("jumping", true);
            }else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                isHurt = true;
            }
        }
    }
}
