using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signboard : MonoBehaviour
{
    public Sprite posterNSignboard;
    private SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;

    public bool inRange = false; //whether player is in range
    public bool yesPoster = true; //whether the signboard has a poster

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    //private void OnCollisionEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Debug.Log("hi");
    //        Level2Gnome gnomeScript = other.gameObject.GetComponent<Level2Gnome>();
    //        bool poster = gnomeScript.havePoster;
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            if (poster == true)
    //            {
    //                //signboardText.gameObject.SetActive(false);
    //                spriteRenderer.sprite = posterNSignboard;
    //            }
    //        }
    //    }    
    //}


}
