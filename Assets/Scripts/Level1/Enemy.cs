using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Tree tree2;
    public Tree tree3;
    public Tree tree4;
    
    public TextMeshProUGUI textMeshText;
    public TextMeshProUGUI textMeshText1;
    public TextMeshProUGUI textMeshText2;
    public TextMeshProUGUI textMeshText3;
    public TextMeshProUGUI textMeshText4;

    [SerializeField]
    private LayerMask playerLayerMask;

    [SerializeField]
    private LayerMask visibilityLayer;
    public bool TargetVisible { get; private set; }

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
        TargetVisible = CheckTargetVisible();
        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= range && TargetVisible == true)
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
    public void TakeDamage(float amount)
    {
        energyBar.slider.value -= amount;

        if (energyBar.slider.value <= 0 && gameObject.name == "Enemy")
        {
            gameObject.SetActive(false);
            tree.gameObject.SetActive(true);
            //tree.transform.position = gameObject.transform.position;

            float posX = gameObject.transform.position.x;
            float posY = gameObject.transform.position.y;

            tree.transform.position = new Vector2(posX, posY + 1);

            textMeshText.gameObject.transform.position = new Vector2(tree.transform.position.x + 1, tree.transform.position.y);
            textMeshText.gameObject.SetActive(true);
        }
        else if (energyBar.slider.value <= 0 && gameObject.name == "Enemy (1)") 
        {
            gameObject.SetActive(false);
            tree1.gameObject.SetActive(true);
            tree1.transform.position = gameObject.transform.position;

            float posX = gameObject.transform.position.x;
            float posY = gameObject.transform.position.y;

            tree1.transform.position = new Vector2(posX, posY + 1);

            textMeshText1.gameObject.transform.position = new Vector2(tree1.transform.position.x + 1, tree1.transform.position.y);
            textMeshText1.gameObject.SetActive(true);
        }

        else if (energyBar.slider.value <= 0 && gameObject.name == "Enemy (2)")
        {
            gameObject.SetActive(false);
            tree2.gameObject.SetActive(true);
            tree2.transform.position = gameObject.transform.position;

            float posX = gameObject.transform.position.x;
            float posY = gameObject.transform.position.y;

            tree2.transform.position = new Vector2(posX, posY + 1);

            textMeshText2.transform.position = new Vector2(tree2.transform.position.x, tree2.transform.position.y);
            textMeshText2.gameObject.SetActive(true);
        }
        else if (energyBar.slider.value <= 0 && gameObject.name == "Enemy (3)")
        {
            gameObject.SetActive(false);
            tree3.gameObject.SetActive(true);
            tree3.transform.position = gameObject.transform.position;

            float posX = gameObject.transform.position.x;
            float posY = gameObject.transform.position.y;

            tree3.transform.position = new Vector2(posX, posY + 1);

            textMeshText3.gameObject.transform.position = new Vector2(tree3.transform.position.x + 1, tree3.transform.position.y);
            textMeshText3.gameObject.SetActive(true);
        }
        else if (energyBar.slider.value <= 0 && gameObject.name == "Enemy (4)")
        {
            gameObject.SetActive(false);
            tree4.gameObject.SetActive(true);
            tree4.transform.position = gameObject.transform.position;

            float posX = gameObject.transform.position.x;
            float posY = gameObject.transform.position.y;

            tree4.transform.position = new Vector2(posX, posY + 1);

            textMeshText4.gameObject.transform.position = new Vector2(tree4.transform.position.x + 1, tree4.transform.position.y);
            textMeshText4.gameObject.SetActive(true);
        }
    }
}
