using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Level2Gnome : MonoBehaviour
{
    Rigidbody2D rb1;
    public float speed1 = 10f;
    public float jumpHeight1 = 10f;
    public bool canDoubleJump1;

    public float timeBetweenShots1, shootSpeed1;
    public float fireRate1;
    float nextFire1;
    public GameObject sticks1;
    public Transform shootPos1;

    public int maxEnergy1 = 30;
    public float seedEnergy = 6;
    public Slider slider;
    public Gradient energyGradient1;

    public bool hasJumped1;
    public bool facingRight1 = true;
    public bool isRunning = false;
    public Animator animator1;

    public EnergyBar energyBar1;
    public Transform groundCheck1;
    public LayerMask groundLayer1;
    public Image fill1;

    private bool moveLeft1;
    private bool moveRight1;
    private float horizontalMove1;
    public TemperatureBar temperatureBar1;
    public GameObject WinScreen1;
    public GameObject loseScreen1;
    private Enemy enemy1;

    //public Collider2D colButton;  //collider at the end of a level
    public GameObject seedText1;
    private TextMeshProUGUI textMeshSeed1;
    public int seedCounts1 = 0;


    public float damageCooldown1;
    [HideInInspector] public float damageTimer1;

    public ArrayList collectArray1 = new ArrayList(); //Arraylist storing collectables

    public void PointerDownLeft()
    {
        moveLeft1 = true;
    }
    public void PointerUpLeft()
    {
        moveLeft1 = false;
    }
    public void PointerDownRight()
    {
        moveRight1 = true;
    }
    public void PointerUpRight()
    {
        moveRight1 = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        energyBar1.SetMaxEnergy(maxEnergy1);
        rb1 = GetComponent<Rigidbody2D>();
        moveLeft1 = false;
        moveRight1 = false;
        textMeshSeed1 = seedText1.GetComponent<TextMeshProUGUI>();
        WinScreen1.gameObject.SetActive(false);

        enemy1 = GetComponent<Enemy>();

    }


    // Update is called once per frame
    void Update()
    {
        CheckMaxTemp1();
        Jumping1();
        MovePlayer1();
        if (energyBar1.slider.value <= 0)
        {
            loseScreen1.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        if (!facingRight1 && moveRight1)
        {
            Flip1();
        }
        if (facingRight1 && moveLeft1)
        {
            Flip1();
        }

        textMeshSeed1.text = "X : " + collectArray1.Count;
        rb1.velocity = new Vector2(horizontalMove1, rb1.velocity.y);

        animator1.SetFloat("PosX", Mathf.Abs(rb1.velocity.x * 10f));
    }
    public bool isGrounded1()
    {
        animator1.SetTrigger("Jump");
        return Physics2D.OverlapCircle(groundCheck1.position, 0.1f, groundLayer1);
    }
    public void Flip1()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        shootSpeed1 *= -1;
        facingRight1 = !facingRight1;
    }
    public void Jumping1()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator1.SetTrigger("Jump");
            if (isGrounded1() || hasJumped1)
            {
                rb1.velocity = new Vector2(rb1.velocity.x, jumpHeight1);
                hasJumped1 = !hasJumped1;
            }
            if (isGrounded1() && !Input.GetButtonDown("Jump"))
            {
                hasJumped1 = false;
            }
            if (Input.GetButtonUp("Jump") && rb1.velocity.y > 0f)
            {
                rb1.velocity = new Vector2(rb1.velocity.x, rb1.velocity.y * 1.5f);
            }
        }
    }
    public void JumpButton1()
    {
        if (isGrounded1())
        {
            rb1.velocity = Vector2.up * jumpHeight1;
            Invoke("EnableDoubleJump1", 0.1f);
        }
        if (canDoubleJump1)
        {
            rb1.velocity = Vector2.up * jumpHeight1;
            canDoubleJump1 = false;
        }
    }
    public void EnableDoubleJump1()
    {
        canDoubleJump1 = true;
    }
    public void AddEnergy1(float energy)
    {
        energyBar1.slider.value += energy;
        energyBar1.SetEnergy(energyBar1.slider.value);
    }

    void MovePlayer1()
    {
        if (moveLeft1)
        {
            horizontalMove1 = -speed1;
            animator1.SetFloat("PosX", Mathf.Abs(rb1.velocity.x * 10f));
        }
        else if (moveRight1)
        {
            horizontalMove1 = speed1;
            animator1.SetFloat("PosX", Mathf.Abs(rb1.velocity.x * 10f));
        }
        else
        {
            horizontalMove1 = 0;
            animator1.SetFloat("PosX", Mathf.Abs(rb1.velocity.x * 10f));
        }
    }
    public void PlayerAttack1()
    {
        StartCoroutine(Shoot1());
    }
    IEnumerator Shoot1()
    {
        if (Time.time > nextFire1)
        {
            nextFire1 = Time.time + fireRate1;
            animator1.SetTrigger("Attack");
            yield return new WaitForSeconds(timeBetweenShots1);
            GameObject newBullet = Instantiate(sticks1, shootPos1.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed1 * speed1, 0);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //}
    void CheckMaxTemp1()
    {
        if (temperatureBar1.slider.value == temperatureBar1.slider.maxValue)
        {
            damageTimer1 -= Time.deltaTime;

            if (damageTimer1 <= 0)
            {
                energyBar1.slider.value -= 1;
                damageTimer1 = damageCooldown1;
            }
        }
    }

    public void TakeDamage1(float amount)
    {
        energyBar1.slider.value -= amount;
        fill1.color = energyGradient1.Evaluate(slider.normalizedValue);

        if (energyBar1.slider.value <= 0)
        {
            loseScreen1.gameObject.SetActive(true);
            gameObject.SetActive(false);

        }
    }

}
