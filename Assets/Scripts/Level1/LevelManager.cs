using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void mainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void level1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }
    public void level2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CutScene");
    }
    //public void level3()
    //{
    //    Time.timeScale = 1;
    //    SceneManager.LoadScene(3);
    //}
}
