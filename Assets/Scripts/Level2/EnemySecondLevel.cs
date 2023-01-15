using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySecondLevel : MonoBehaviour
{
    public float range;
    public float walkSpeed = 10;
    public float bulletSpeed = 30;

    private Rigidbody2D rb2d;
    private bool patrol;
    private bool canShoot;
    public float timeBetweenShots, shootSpeed;
    public CapsuleCollider2D bodyCollider;
    public GameObject bullet;
    public Transform shootPos;
    Level2Gnome player;
    public bool mustFlip;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public EnergyBar energyBar;
    public bool TargetVisible { get; private set; }
    [SerializeField]
    private LayerMask playerLayerMask;

    [SerializeField]
    private LayerMask visibilityLayer;

    public GameObject tree;
    public GameObject treeFire;
    public GameObject tree1;
    public GameObject treeFire1;
    public GameObject tree2;
    public GameObject treeFire2;
    public GameObject tree3;
    public GameObject treeFire3;
    public GameObject tree4;
    public GameObject treeFire4;
    public GameObject tree5;
    public GameObject treeFire5;
    public Level2Gnome level2Gnome;

    public GameObject enemy;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        patrol = true;
        canShoot = true;
        bodyCollider = GetComponent<CapsuleCollider2D>();
        player = FindObjectOfType<Level2Gnome>();


        Physics2D.IgnoreLayerCollision(7, 9);
        
        level2Gnome = GetComponent<Level2Gnome>();

        Physics2D.IgnoreLayerCollision(7, 10);
        StartCoroutine(SpawnEnemies());
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
        TargetVisible = CheckTargetVisible();

        /*float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distToPlayer <= range && TargetVisible == true)
        {
            if (player.transform.position.x > transform.position.x && transform.localScale.x < 0 || player.transform.position.x < transform.position.x && transform.localScale.x > 0) 
            {
                Flip();
            }
            patrol = false;
            rb2d.velocity = Vector2.zero;

            if (canShoot) 
            {
                //StartCoroutine(Shoot());
            }
        }
        else 
        {
            patrol = true;
        }*/
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
    IEnumerator SpawnEnemies() 
    {
        
        enemy.SetActive(true);
        yield return new WaitForSeconds(10);
        enemy1.SetActive(true);
        yield return new WaitForSeconds(10);
        enemy2.SetActive(true);
        yield return new WaitForSeconds(10);
        enemy3.SetActive(true);
        yield return new WaitForSeconds(10);
        enemy4.SetActive(true);
        yield return new WaitForSeconds(10);
        
    }
    private void Disable() 
    {
        gameObject.SetActive(false);
    }
    private bool CheckTargetVisible()
    {
        //check the visibility of the player or children
        var result = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 11, visibilityLayer);
        if (result.collider != null)
        {
            //this will let the zombies not able to detect the player or children who are behind the walls
            return (playerLayerMask & (1 << result.collider.gameObject.layer)) != 0;
        }
        return false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tree")
        {
            tree.gameObject.SetActive(false);
            treeFire.SetActive(true);
            animator.SetBool("Fire", true);
            patrol = false;
            Invoke("Disable", 1.5f);
        }
        if (collision.gameObject.name == "Tree1")
        {
            tree1.gameObject.SetActive(false);
            treeFire1.SetActive(true);
            animator.SetBool("Fire", true);
            patrol = false;
            Invoke("Disable", 1.5f);
        }
        if (collision.gameObject.name == "Tree2")
        {
            tree2.gameObject.SetActive(false);
            treeFire2.SetActive(true);
            animator.SetBool("Fire", true);
            patrol = false;
            Invoke("Disable", 1.5f);
        }
        if (collision.gameObject.name == "Tree3")
        {
            tree3.gameObject.SetActive(false);
            treeFire3.SetActive(true);
            animator.SetBool("Fire", true);
            patrol = false;
            Invoke("Disable", 1.5f);
        }
        if (collision.gameObject.name == "Tree4")
        {
            tree4.gameObject.SetActive(false);
            treeFire4.SetActive(true);
            animator.SetBool("Fire", true);
            patrol = false;
            Invoke("Disable", 1.5f);
        }
        if (collision.gameObject.name == "Tree5")
        {
            tree5.gameObject.SetActive(false);
            treeFire5.SetActive(true);
            animator.SetBool("Fire", true);
            patrol = false;
            Invoke("Disable", 1.5f);
        }

        if (collision.gameObject.tag == "Finish") 
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.layer == 11)
        {
            Signboard signboard = GetComponent<Signboard>();
            bool checkSignboard = signboard.yesPoster;
            Debug.Log("a");
            if (checkSignboard == false)
                signboard.boxCollider2D.enabled = false;
                
        }
       
    }
    /*public void TakeDamageFire(float amount) 
    {
        energyBar.slider.value -= amount;
        if (energyBar.slider.value <= 0)
        {
            Destroy(gameObject);
        }
        print("Took dmg");
    }*/
}
