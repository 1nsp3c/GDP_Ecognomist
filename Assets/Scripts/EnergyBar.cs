using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public Gradient healthGradient;
    public Image fill;
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = healthGradient.Evaluate(slider.normalizedValue);
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = healthGradient.Evaluate(1f);
    }
}
