using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureBar : MonoBehaviour
{
    public Slider slider;
    public float fillTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        FillSlider();
        if (Input.GetKeyDown(KeyCode.E))
        {
            fillTime = 0f;
        }
    }
    public void FillSlider()
    {
        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, fillTime);

        fillTime += 0.375f * Time.deltaTime;
    }

    public void ResetSlider()
    {
        fillTime = 0f;
    }
}
