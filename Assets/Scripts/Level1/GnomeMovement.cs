using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class GnomeMovement : MonoBehaviour
{

    public float timeBetweenShots, shootSpeed;
    public float fireRate;
    float nextFire;
    public GameObject sticks;
    public Transform shootPos;

    public int maxEnergy = 30;
    public float seedEnergy = 6;
    public Slider slider;
    public Gradient energyGradient;


    public EnergyBar energyBar;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Image fill;

    [Header("Canvas")]
    public GameObject WinScreen;
    public GameObject loseScreen;
    private SpriteRenderer spriteRend;

    [Header("Movement")]
    bool moveLeft;
    bool moveRight;
    float horizontalMove;
    bool hasJumped;
    bool facingRight = true;
    bool isRunning = false;
    Rigidbody2D rb;
    float speed = 10f;
    float jumpHeight = 16f;
    bool canDoubleJump;
    public Animator animator;
    public TemperatureBar temperatureBar;
    Enemy enemy;

    [Header("Seeds")]
    //public Collider2D colButton;  //collider at the end of a level
    public GameObject seedText;
    private TextMeshProUGUI textMeshSeed;
    private int seedCounts = 0;
    public bool seedPlanted;

    [Header("Trees")]
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

    public float damageCooldown;
    [HideInInspector] public float damageTimer;

    public ArrayList collectArray = new ArrayList(); //Arraylist storing collectables

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
        energyBar.SetMaxEnergy(maxEnergy);
        rb = GetComponent<Rigidbody2D>();
        moveLeft = false;
        moveRight = false;
        textMeshSeed = seedText.GetComponent<TextMeshProUGUI>();
        WinScreen.gameObject.SetActive(false);
        spriteRend = GetComponent<SpriteRenderer>();

        enemy = GetComponent<Enemy>();
        
    }


    // Update is called once per frame
    void Update()
    {
        CheckMaxTemp();
        Jumping();
        MovePlayer();
        if (energyBar.slider.value <= 0)
        {
            loseScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        if (!facingRight && moveRight)
        {
            Flip();
        }
        if (facingRight && moveLeft)
        {
            Flip();
        }

        textMeshSeed.text = "X : " + collectArray.Count;
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
        shootSpeed *= -1;
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
    public void AddEnergy(float energy)
    {
        energyBar.slider.value += energy;
        energyBar.SetEnergy(energyBar.slider.value);
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
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(timeBetweenShots);
            GameObject newBullet = Instantiate(sticks, shootPos.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * speed, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collectArray.Count < 1) //Limits the player to only be able to collect 1 seed at a time
        {
            if (collision.gameObject.tag == "Collectables")  //tags with the name collectables
            {
                AddEnergy(5);
                //temperatureBar.ResetSlider();
                Destroy(collision.gameObject); //destroy collectable

                collectArray.Add(collision.gameObject); //Adds the seed_bag into the Arraylist
            }
        }



        if (collision.gameObject.name == "Tree" && collectArray.Count == 1)
        {
            collectArray.Clear(); //Removes all elements from the arraylist
            tree.animator.SetTrigger("ASD");
            //temperatureBar.fillTime /= 3f;
            tree.boxCollider2D.enabled = false;
            StartCoroutine(TempSpeedReduction());
            textMeshText.gameObject.SetActive(false);
            seedCounts += 1;
        }

        else if (collision.gameObject.name == "Tree (1)" && collectArray.Count == 1)
        {
            collectArray.Clear();
            //temperatureBar.fillTime /= 3f;
            tree1.animator.SetTrigger("ASD");
            tree1.boxCollider2D.enabled = false;
            StartCoroutine(TempSpeedReduction());
            textMeshText1.gameObject.SetActive(false);
            seedCounts += 1;
        }
        else if (collision.gameObject.name == "Tree (2)" && collectArray.Count == 1)
        {
            collectArray.Clear();
            //temperatureBar.fillTime /= 3f;
            tree2.animator.SetTrigger("ASD");
            tree2.boxCollider2D.enabled = false;
            StartCoroutine(TempSpeedReduction());
            textMeshText2.gameObject.SetActive(false);
            seedCounts += 1;
        }
        else if (collision.gameObject.name == "Tree (3)" && collectArray.Count == 1)
        {
            collectArray.Clear();
            //temperatureBar.fillTime /= 3f;
            tree3.animator.SetTrigger("ASD");
            tree3.boxCollider2D.enabled = false;
            StartCoroutine(TempSpeedReduction());
            textMeshText3.gameObject.SetActive(false);
            seedCounts += 1;
        }
        else if (collision.gameObject.name == "Tree (4)" && collectArray.Count == 1)
        {
            collectArray.Clear();
            //temperatureBar.fillTime /= 3f;
            tree4.animator.SetTrigger("ASD");
            tree4.boxCollider2D.enabled = false;
            StartCoroutine(TempSpeedReduction());
            textMeshText4.gameObject.SetActive(false);
            seedCounts += 1;
        }


        if (seedCounts == 5)
        {
            Time.timeScale = 0;
            WinScreen.gameObject.SetActive(true);
            rb.gameObject.SetActive(false);

        }

        if (collision.gameObject.layer == 6)
        {
            loseScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    void CheckMaxTemp()
    {
        if (temperatureBar.slider.value == temperatureBar.slider.maxValue)
        {
            damageTimer -= Time.deltaTime;

            if (damageTimer <= 0)
            {
                energyBar.slider.value -= 1;
                StartCoroutine(FlashRed());
                fill.color = energyGradient.Evaluate(slider.normalizedValue);
                damageTimer = damageCooldown;
            }
        }
    }
    public void TakeDamage(float amount) 
    {
        energyBar.slider.value -= amount;
        StartCoroutine(FlashRed());
        fill.color = energyGradient.Evaluate(slider.normalizedValue);

        if (energyBar.slider.value <= 0) 
        {
            loseScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public IEnumerator FlashRed()
    {
        spriteRend.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.2f);
        spriteRend.color = Color.white;
        yield return new WaitForSeconds(0.2f);
    }
    public IEnumerator TempSpeedReduction()
    {
        seedPlanted = true;
        yield return new WaitForSeconds(0.1f);
        seedPlanted = false;
    }
}
