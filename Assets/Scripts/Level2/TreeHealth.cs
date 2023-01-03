using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TreeHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient energyGradient;
    public Image fill;
    private SpriteRenderer spriteRend;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = 300;
    }

    public void SetEnergy(float energy)
    {
        healthSlider.value = energy;
        fill.color = energyGradient.Evaluate(healthSlider.normalizedValue);
    }
    public void SetMaxEnergy(int energy)
    {
        healthSlider.maxValue = energy;
        healthSlider.value = energy;

        fill.color = energyGradient.Evaluate(1f);
    }
    public void TakeDamage(float amount)
    {
        healthSlider.value -= amount;
        StartCoroutine(FlashRed());
        fill.color = energyGradient.Evaluate(healthSlider.normalizedValue);

        //if (healthSlider.value <= 0)
        //{
        //    loseScreen.gameObject.SetActive(true);
        //    gameObject.SetActive(false);

        //}
    }
    public IEnumerator FlashRed()
    {
        spriteRend.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.2f);
        spriteRend.color = Color.white;
        yield return new WaitForSeconds(0.2f);
    }
}
