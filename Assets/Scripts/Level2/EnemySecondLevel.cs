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

    public Tree[] treeList;
    public GameObject tree;
    public Level2Gnome level2Gnome;

    public Signboard signboard3;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        patrol = true;
        canShoot = true;
        bodyCollider = GetComponent<CapsuleCollider2D>();
        player = FindObjectOfType<Level2Gnome>();
        //energyBar.SetMaxEnergy(30);
        Physics2D.IgnoreLayerCollision(7, 9);
        treeList = FindObjectsOfType<Tree>();
        level2Gnome = GetComponent<Level2Gnome>();

        Physics2D.IgnoreLayerCollision(7, 10);
        //Physics2D.IgnoreLayerCollision(7, 11);
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
        if (collision.gameObject.tag == "Tree")
        {
            tree.gameObject.SetActive(false);
            //treeFire.SetActive(true);
        }

        if (collision.gameObject.tag == "Finish") 
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.name == "SignBoard (3)")
        {
            signboard3.boxCollider2D.enabled = false;
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
