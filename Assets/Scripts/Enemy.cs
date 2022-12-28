using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float walkSpeed, range, timeBetweenShots, shootSpeed;
    private float distToPlayer;
    public bool patrol;
    private bool mustFlip, canShoot;
    private Rigidbody2D rb2d;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform player, shootPos;
    public GameObject bullet;
    public EnergyBar energyBar;
    public int maxEnergy = 30;
    public Tree tree;
    public Tree tree1;

    // Start is called before the first frame update
    void Start()
    {
        patrol = true;
        rb2d = GetComponent<Rigidbody2D>();
        canShoot = true;
        energyBar.SetMaxEnergy(maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol) 
        {
            Patrol();

        }
        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= range)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            patrol = false;
            rb2d.velocity = Vector2.zero;

            if(canShoot)
            StartCoroutine(Shoot());
        }
        else 
        {
            patrol = true;
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

    IEnumerator Shoot() 
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);

        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed * Time.fixedDeltaTime, 0);
        canShoot = true;

    }
    public void TakeDamage(float amount)
    {
        energyBar.slider.value -= amount;

        if (energyBar.slider.value <= 0 && gameObject.name == "Enemy")
        {
            gameObject.SetActive(false);
            tree.gameObject.SetActive(true);
            tree.transform.position = gameObject.transform.position;
        }
        else if (energyBar.slider.value <= 0 && gameObject.name == "Enemy (1)") 
        {
            gameObject.SetActive(false);
            tree1.gameObject.SetActive(true);
            tree1.transform.position = gameObject.transform.position;
        }
    }
}
