using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySecondLevel : MonoBehaviour
{
    public float range;
    private float walkSpeed = 10;
    //public GameObject tree;
    private Rigidbody2D rb2d;
    private bool patrol;
    private bool canShoot;
    public float timeBetweenShots, shootSpeed;
    public CapsuleCollider2D bodyCollider;
    public GameObject bullet;
    public Transform shootPos;
    public Level2Gnome player;
    public bool mustFlip;
    public Transform groundCheck;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        patrol = true;
        canShoot = true;
        bodyCollider = GetComponent<CapsuleCollider2D>();
        player = FindObjectOfType<Level2Gnome>();
        
    }
    private void FixedUpdate()
    {
        if (patrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (patrol == true) 
        {
            Patrol();
        }
        
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distToPlayer <= range)
        {
            patrol = false;
            rb2d.velocity = Vector2.zero;

            if (canShoot) 
            {
                StartCoroutine(Shoot());
            }
        }
        else 
        {
            patrol = true;
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
        bullet.transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
    IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);

        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed * Time.fixedDeltaTime, 0);
        canShoot = true;

    }
}
