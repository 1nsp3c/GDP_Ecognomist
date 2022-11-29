using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10f;
    public float jumpHeight = 10f;
    public bool canDoubleJump;

    public int maxHealth = 20;
    public int currentHealth;

    public bool hasJumped;
    public bool facingRight = true;
    public bool isRunning = false;
    public Animator animator;

    public HealthBar healthBar;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;

    public void PointerDownLeft() 
    {
        moveLeft = true;
    }
    public void PointerUpLeft() 
    {
        moveLeft = false;
    }
    public void PointerDownRight() 
    {
        moveRight = true;
    }
    public void PointerUpRight() 
    {
        moveRight = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        moveLeft = false;
        moveRight = false;
    }


    // Update is called once per frame
    void Update()
    {
        Jumping();
        MovePlayer();
        if (!facingRight && moveRight)
        {
            Flip();
        }
        if (facingRight && moveLeft)
        {
            Flip();
        }
        
        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);

        animator.SetFloat("PosX", Mathf.Abs(rb.velocity.x * 10f));
    }
    public bool isGrounded()
    {
        animator.SetTrigger("Jump");
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    public void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        facingRight = !facingRight;
    }
    public void Jumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
            if (isGrounded() || hasJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                hasJumped = !hasJumped;
            }
            if (isGrounded() && !Input.GetButtonDown("Jump"))
            {
                hasJumped = false;
            }
            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 1.5f);
            }
        }
    }
    public void JumpButton()
    {
        if (isGrounded())
        {
            rb.velocity = Vector2.up * jumpHeight;
            Invoke("EnableDoubleJump", 0.1f);
        }
        if (canDoubleJump)
        {
            rb.velocity = Vector2.up * jumpHeight;
            canDoubleJump = false;
        }
    }
    public void EnableDoubleJump()
    {
        canDoubleJump = true;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void MovePlayer() 
    {
        if (moveLeft)
        {
            horizontalMove = -speed;
            animator.SetFloat("PosX", Mathf.Abs(rb.velocity.x * 10f));
        }
        else if (moveRight)
        {
            horizontalMove = speed;
            animator.SetFloat("PosX", Mathf.Abs(rb.velocity.x * 10f));
        }
        else 
        {
            horizontalMove = 0;
            animator.SetFloat("PosX", Mathf.Abs(rb.velocity.x * 10f));
        }
    }
    public void PlayerAttack()
    {
        animator.SetTrigger("Attack");
    }
}
