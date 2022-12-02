using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject WinScreen;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        WinScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.tag == "Player")
        {
            WinScreen.gameObject.SetActive(true);
            rb.gameObject.SetActive(false);
        }
    }
}
