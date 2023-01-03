using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    //[SerializeField] private float timerSpeed = 2f;
    //private float elapsed;

    float time = 180f;

    //public Text timerText;
    public GameObject TimerObj;
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = TimerObj.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(time > 0f)
        {
            time -= 1 * Time.deltaTime;
        }
        else
        {
            time = 0f;
            
        }
        DisplayTime(time);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0f;
        }

        //Calculates the number of minutes using time/60
        float minutes = Mathf.FloorToInt(timeToDisplay / 60f);
        //Calculates the number of seconds using the remainder of time/60
        float seconds = Mathf.FloorToInt(timeToDisplay % 60f);

        timerText.text = (minutes + " : " + seconds);
    }
}
