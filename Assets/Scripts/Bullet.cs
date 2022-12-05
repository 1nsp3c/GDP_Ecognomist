using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dieTime, damage;
    public GameObject dieEffect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GnomeMovement gnomeMovement = collision.gameObject.GetComponent<GnomeMovement>();
        if (gnomeMovement != null) 
        {
            gnomeMovement.TakeDamage(damage);
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
