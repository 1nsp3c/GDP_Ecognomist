using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }
    public void levelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
}
