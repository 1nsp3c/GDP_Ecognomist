using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    //public Collider2D colButton;  //collider at the end of a level
    public GameObject seedText;
    private TextMeshProUGUI textMeshSeed;

    ArrayList collectArray = new ArrayList(); //Arraylist storing collectables

    // Start is called before the first frame update
    void Start()
    {
        textMeshSeed = seedText.GetComponent<TextMeshProUGUI>(); //Grabs the TextMeshProGui of the seed Texts
    }


    // Update is called once per frame
    void Update()
    {
        textMeshSeed.text = "Count : " + collectArray.Count;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collectables")  //tags with the name collectables
        {
            Destroy(collision.gameObject);          //destroy collectable
            //colButton.GetComponent<Collider>().isTrigger = true;  //collider on tirgger is enabled

            collectArray.Add(collision.gameObject); //Adds the seed_bag into the Arraylist
        }
    }
}