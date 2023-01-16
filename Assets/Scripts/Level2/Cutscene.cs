using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cutscene : MonoBehaviour
{
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
        if (collision.gameObject.tag == "Tree") 
        {
            Invoke("ChangeScene", 1.5f);
        }
    }

    public void ChangeScene() 
    {
        SceneManager.LoadScene("EnemySecondLevel");
    }
}
