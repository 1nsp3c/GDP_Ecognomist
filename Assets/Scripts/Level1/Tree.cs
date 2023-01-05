using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int timesHit = 0;
    public bool extinguish = false;

    public Animator animator;
    public BoxCollider2D boxCollider2D;

    public List<GameObject> treeList = new List<GameObject>();
    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(9, 10); //Ignore collision with player
    }

    private void Update()
    {
        if (timesHit >= 3)
        {
            extinguish = true;
            animator.SetBool("extinguished", extinguish);
        }
    }
}
