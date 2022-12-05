using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class GnomeMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10f;
    public float jumpHeight = 10f;
    public bool canDoubleJump;

    public int maxEnergy = 20;
    public float seedEnergy = 6;

    public bool hasJumped;
    public bool facingRight = true;
    public bool isRunning = false;
    public Animator animator;

    public EnergyBar energyBar;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    public TemperatureBar temperatureBar;
    public GameObject WinScreen;
    public GameObject loseScreen;

    //public Collider2D colButton;  //collider at the end of a level
    public GameObject seedText;
    private TextMeshProUGUI textMeshSeed;

    public Tree tree;

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
        
    }


    // Update is called once per frame
    void Update()
    {
        CheckMaxTemp();
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
        animator.SetTrigger("Attack");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collectables")  //tags with the name collectables
        {
            AddEnergy(10);
            temperatureBar.ResetSlider();
            Destroy(collision.gameObject);          //destroy collectable
            //colButton.GetComponent<Collider>().isTrigger = true;  //collider on tirgger is enabled

            collectArray.Add(collision.gameObject); //Adds the seed_bag into the Arraylist
        }
        if (collision.gameObject.tag == "Tree")
        {
            plantTree();
            if (collectArray.Count == 5)
            { 
                WinScreen.gameObject.SetActive(true);
                rb.gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.layer == 6)
        {
            loseScreen.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
    private void plantTree()
    {
        int seedCount = collectArray.Count;

        for (int i = 0; i < seedCount; i++)
        {
            tree.treeList[i].SetActive(true);
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

                damageTimer = damageCooldown;
            }
        }
    }

    public void TakeDamage(float amount) 
    {
        energyBar.slider.value -= amount;

        if (energyBar.slider.value <= 0) 
        {
            loseScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
