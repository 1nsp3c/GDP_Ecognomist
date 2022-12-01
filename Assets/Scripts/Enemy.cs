using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float walkSpeed;
    public bool patrol;
    private bool mustFlip;
    private Rigidbody2D rb2d;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        patrol = true;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol) 
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (patrol) 
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }
    }
    void Patrol() 
    {
        if (mustFlip || bodyCollider.IsTouchingLayers())  
        {
            Flip();
        }
        rb2d.velocity = new Vector2(walkSpeed, rb2d.velocity.y);
    }

    void Flip() 
    {
        patrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        patrol = true; 
    }
}
