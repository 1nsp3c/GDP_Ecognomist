using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2TempBar : MonoBehaviour
{
    public Slider slider;
    public float fillTime = 0f;
    public Gradient tempGradient;
    public Image fill;
    public Level2Gnome level2Gnome;

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
        //fillTime += 0.4f * Time.deltaTime;
        fillTime += 0.04f * Time.deltaTime;
        slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, fillTime);

        fill.color = tempGradient.Evaluate(slider.normalizedValue);
    }

    public void ResetSlider()
    {
        fillTime = 0f;
    }
}
