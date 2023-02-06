using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject helpPage;
    public GameObject general;
    public GameObject level1;
    public GameObject level2;

    public int currentIndex = 0;

    private bool helpActive = false;


    // Start is called before the first frame update

    public void Update()
    {
        if (helpActive)
        {
            if (currentIndex == 0)
            {
                general.SetActive(true);
                level1.SetActive(false);
                level2.SetActive(false);
            }
            if (currentIndex == 1)
            {
                general.SetActive(false);
                level1.SetActive(true);
                level2.SetActive(false);
            }
            if (currentIndex == 2)
            {
                general.SetActive(false);
                level1.SetActive(false);
                level2.SetActive(true);
            }
        }
        else
        {
            general.SetActive(false);
            level1.SetActive(false);
            level2.SetActive(false);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level Select");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void HelpPage()
    {
        helpPage.SetActive(true);
        helpActive = true;
    }

    public void Close()
    {
        helpPage.SetActive(false);
        helpActive = false;
    }

    public void Next()
    {
        currentIndex += 1;
    }
    public void Previous()
    {
        currentIndex -= 1;
    }
}
