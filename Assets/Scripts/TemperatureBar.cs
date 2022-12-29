using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureBar : MonoBehaviour
{
    public Slider slider;
    public float fillTime = 0f;
    public Gradient tempGradient;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        FillSlider();
    }
    public void FillSlider()
    {
        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, fillTime);

        fillTime += 0.15f * Time.deltaTime;
        fill.color = tempGradient.Evaluate(slider.normalizedValue);
    }

    public void ResetSlider()
    {
        fillTime = 0f;
    }
}
