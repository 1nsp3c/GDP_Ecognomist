using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxCollider2D;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    public List<GameObject> treeList = new List<GameObject>();
}
