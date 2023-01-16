using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2TempBar : MonoBehaviour
{
    public Slider slider;
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
        fill.color = tempGradient.Evaluate(slider.normalizedValue);
    }
    public void AddValue()
    {
        slider.value += 100f;
    }
}
