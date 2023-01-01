using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySecondLevel : MonoBehaviour
{
    public float range;
    private float walkSpeed = 10;
    public Transform tree;
    private Rigidbody2D rb2d;
    public bool patrol;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        patrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol == true) 
        {
            Patrol();
        }
        
        float distToPlayer = Vector2.Distance(transform.position, tree.transform.position);

        if (distToPlayer <= range)
        {
            patrol = false;
            rb2d.velocity = Vector2.zero;

            //if (canShoot)
            //StartCoroutine(Shoot());
        }
        else 
        {
            patrol = true;
        }
    }
    void Patrol()
    {
        rb2d.velocity = new Vector2(walkSpeed, rb2d.velocity.y);
    }
}
