using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignBoardText : MonoBehaviour
{
    public TextMeshProUGUI signboardText;
    public Signboard Signboard; //takes respective signboard
    // Start is called before the first frame update
    void Start()
    {
        signboardText.gameObject.SetActive(false);
        Signboard = gameObject.GetComponentInParent<Signboard>();
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
            Signboard.inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            signboardText.gameObject.SetActive(false);
            Signboard.inRange = false;
        }
    }
}
