using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignBoards : MonoBehaviour
{
    public TextMeshProUGUI signboardText;
    // Start is called before the first frame update
    void Start()
    {
        signboardText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            signboardText.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            signboardText.gameObject.SetActive(false);
        }
    }
}
