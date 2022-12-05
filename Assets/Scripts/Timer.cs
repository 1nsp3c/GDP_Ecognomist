using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    //[SerializeField] private float timerSpeed = 2f;
    //private float elapsed;
    float currentTime = 0f;
    float startingTime = 0f;

    //public Text timerText;
    public TextMeshPro timerText;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("F2");
    }


    // Update is called once per frame
    //private void Update()
    //{
    //    elapsed += Time.deltaTime;
    //    if(elapsed >= timerSpeed)
    //    {
    //        elapsed = 0f;
    //        //GetComponent<Flasher>().Flash;
    //    }
    //}
}
