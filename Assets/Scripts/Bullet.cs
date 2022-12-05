using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dieTime, damage;
    public GameObject dieEffect;
    public EnergyBar energyBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            energyBar.slider.value -= 50;
            Die();
        }
        
    }
    IEnumerator CountDownTimer() 
    { 
        yield return new WaitForSeconds(dieTime);
        Die();
    }
    void Die() 
    {
        Destroy(gameObject);
    }
}
