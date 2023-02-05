using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void levelSelect()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level Select");
    }
    public void Cutscene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CutScene");
    }
}
