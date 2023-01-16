using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public float walkSpeed = 10;

    private Rigidbody2D rb2d;
    private bool patrol;
    public CapsuleCollider2D bodyCollider;
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

    public Level2Gnome level2Gnome;


    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        patrol = true;
        bodyCollider = GetComponent<CapsuleCollider2D>();
        player = FindObjectOfType<Level2Gnome>();


        Physics2D.IgnoreLayerCollision(7, 9);

        level2Gnome = GetComponent<Level2Gnome>();

        Physics2D.IgnoreLayerCollision(7, 10);

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

    private void Disable()
    {
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tree5")
        {
            tree.gameObject.SetActive(false);
            treeFire.SetActive(true);
            animator.SetBool("Fire", true);
            patrol = false;
            Invoke("Disable", 1.5f);
        }


        if (collision.gameObject.tag == "Finish")
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Level2");
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

}
