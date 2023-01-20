using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterFactory : MonoBehaviour
{
    private Level2Gnome level2Gnome;
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        level2Gnome = collision.gameObject.GetComponent<Level2Gnome>();
        Debug.Log("Poster");
        level2Gnome.havePoster = true;
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    level2Gnome = collision.gameObject.GetComponent<Level2Gnome>();
    //    Debug.Log("Poster");
    //    level2Gnome.havePoster = true;
    //}
}
