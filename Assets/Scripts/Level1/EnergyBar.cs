using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public Gradient energyGradient;
    public Image fill;

    private void Start()
    {
        slider.value = 300;
    }
    public void SetEnergy(float energy)
    {
        slider.value = energy;

        fill.color = energyGradient.Evaluate(slider.normalizedValue);
    }
    public void SetMaxEnergy(int energy)
    {
        slider.maxValue = energy;
        slider.value = energy;

        fill.color = energyGradient.Evaluate(1f);
    }
}
