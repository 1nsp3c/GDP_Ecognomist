using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignBoards : MonoBehaviour
{
    public TextMeshProUGUI signboardText;
    public GameObject signNposter;
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
            Level2Gnome gnomeScript = collision.gameObject.GetComponent<Level2Gnome>();
            bool poster = gnomeScript.havePoster;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (poster == true)
                {
                    signboardText.gameObject.SetActive(false);
                    Instantiate(signNposter, new Vector3(-52f, -1.75f, 0f), Quaternion.identity);

                }
            }
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
