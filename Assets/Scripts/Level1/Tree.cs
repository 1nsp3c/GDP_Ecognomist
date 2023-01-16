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
    Level2TempBar level2TempBar;
    Level2Gnome level2Gnome;

    public List<GameObject> treeList = new List<GameObject>();
    private void Start()
    {
        level2TempBar = FindObjectOfType<Level2TempBar>();
        level2Gnome = FindObjectOfType<Level2Gnome>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(9, 10); //Ignore collision with player
    }

    private void Update()
    {
    }
    public void ResetTempBar()
    {
        if (timesHit == 3)
        {
            extinguish = true;
            level2Gnome.AddEnergy1(6);
            boxCollider2D.enabled = false;
            animator.SetBool("extinguished", extinguish);
        }
    }
}
