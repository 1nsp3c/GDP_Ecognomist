using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    //public Collider2D colButton;  //collider at the end of a level

    ArrayList collectArray = new ArrayList(); //Arraylist storing collectables

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {

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
