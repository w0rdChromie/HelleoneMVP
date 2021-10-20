using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCont : MonoBehaviour
{
    public float speed;
    public float jump;
    private float moveInput;

    private Rigidbody2D rb2d;
    private Animator anim;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask isGround;

    public int moreJumps;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, isGround);

        moveInput = Input.GetAxis("Horizontal");
        Debug.Log(moveInput);
        rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
        if (facingRight == false && moveInput > 0) {

            Flip();
        
        }
        else if (facingRight == true && moveInput < 0) {

            Flip();

        }
    }

    void Update() {

        if (isGrounded == true) {

            moreJumps = 1;
        
        }

        if (Input.GetKeyDown(KeyCode.Space) && moreJumps > 0) {
            rb2d.velocity = Vector2.up * jump;
            moreJumps--;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {

            anim.Play("atk");

        }
        else {

            anim.Play("idle");
        
        }

    }

    void Flip() {

        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    
    }
}
