using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCont : MonoBehaviour
{
    //basics
    public float speed;
    public float jump;
    private float moveInput;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool facingRight = true;
    private bool canMove = true;
    public int maxHealth = 100;
    public int currentHealth;
    public healthBar hb;
    private bool isRunning;
    //jumping
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask isGround;
    public int moreJumps;
    //attack
    private float cooldown = 0f;
    private bool reset = true;
    private int atkUp = 1;
    private float combo = 0;
    //teleport
    private Vector2 target;
    private Transform playerPos;
    private float teleCooldown = 0f;
    //dash
    private float currentSpd;
    public float dashSpd;
    public float dashLength = .5f;
    public float dashCooldown = 1f;
    private float dashCount;
    private float dashCoolCount;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentSpd = speed;
        currentHealth = maxHealth;
        hb.setMaxHealth(maxHealth);
    }

    void FixedUpdate() {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, isGround);

        moveInput = Input.GetAxis("Horizontal");
        //Debug.Log(moveInput);
        if (canMove) { rb2d.velocity = new Vector2(moveInput * currentSpd, rb2d.velocity.y); }
        if (facingRight == false && moveInput > 0) {

            Flip();
        
        }
        else if (facingRight == true && moveInput < 0) {

            Flip();

        }
    }

    void Update() {

        canMove = cooldown <= 0;
        combo -= Time.deltaTime;

        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            bool isRunning = true;
            if (isGrounded)
            {

                anim.SetBool("running", isRunning);

            }

        }
        else {

            isRunning = false;
            anim.SetBool("running", isRunning);

        }

        if (combo <= 0)
        {
            anim.SetBool("atkReset", true);
        }
        else {
            anim.SetBool("atkReset", false);
        }

        if (transform.position.y < -12 || currentHealth == 0) {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

        cooldown -= Time.deltaTime;
        teleCooldown -= Time.deltaTime;

        anim.SetBool("grounded", isGrounded);

        if (isGrounded == true) {

            moreJumps = 1;
        
        }

        if (Input.GetKeyDown(KeyCode.W) && moreJumps > 0) {
            anim.SetTrigger("jumpTrig");
            rb2d.velocity = Vector2.up * jump;
            moreJumps--;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCount <= 0 && dashCount <= 0) {

                currentSpd = dashSpd;
                dashCount = dashLength;

            }
        }

        if (dashCount > 0) {

            dashCount -= Time.deltaTime;
            if (dashCount <= 0) {

                currentSpd = speed;
                dashCoolCount = dashCooldown;
            
            }
        
        }

        if (dashCoolCount > 0) {

            dashCoolCount -= Time.deltaTime;
        
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (cooldown <= 0)
            {
                switch (atkUp)
                {

                    case 1:
                        anim.SetTrigger("attack");
                        cooldown = 0.1f;
                        atkUp++;
                        reset = false;
                        if (facingRight)
                        {
                            rb2d.AddForce(new Vector2(5f, 0), ForceMode2D.Impulse);
                        }
                        else
                        {
                            rb2d.AddForce(new Vector2(-5f, 0), ForceMode2D.Impulse);
                        }
                        combo = 1;
                        break;

                    case 2:
                        anim.SetTrigger("attack");
                        cooldown = 0.2f;
                        atkUp++;
                        if (facingRight)
                        {
                            rb2d.AddForce(new Vector2(5f, 0), ForceMode2D.Impulse);
                        }
                        else
                        {
                            rb2d.AddForce(new Vector2(-5f, 0), ForceMode2D.Impulse);
                        }
                        combo = 1;
                        break;
                    case 3:
                        anim.SetTrigger("attack");
                        cooldown = 0.3f;
                        atkUp -= 2;
                        reset = true;
                        anim.SetBool("atkReset", reset);
                        if (facingRight)
                        {
                            rb2d.AddForce(new Vector2(5f, 0), ForceMode2D.Impulse);
                        }
                        else
                        {
                            rb2d.AddForce(new Vector2(-5f, 0), ForceMode2D.Impulse);
                        }
                        combo = 0;
                        break;

                }


            }

        }


        if (Input.GetMouseButtonDown(1) && teleCooldown <= 0)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerPos.position = new Vector2(target.x, target.y);
            anim.SetBool("grounded", false);
            teleCooldown = 4f;
        }

    }

    void Flip()
    {

        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }

    public void TakeDamage(int damage) {

        currentHealth -= damage;
        hb.setHealth(currentHealth);
    
    }

    
}
