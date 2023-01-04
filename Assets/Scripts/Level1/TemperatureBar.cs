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
    public GnomeMovement gnomeMovement;

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
        //ReduceTempSpeed();
    }
    public void FillSlider()
    {
        fillTime += 0.04f * Time.deltaTime;
        if (gnomeMovement.seedPlanted == true)
        {
            fillTime -= 0.01f;
        }
        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, fillTime);

        fill.color = tempGradient.Evaluate(slider.normalizedValue);
    }

    public void ResetSlider()
    {
        fillTime = 0f;
    }
    //public void ReduceTempSpeed()
    //{
    //    FillSlider();
    //    if (gnomeMovement.seedCounts == 1)
    //    {

    //    }
    //}
}
