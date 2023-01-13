using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signboard : MonoBehaviour
{
    public Sprite posterNSignboard;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
