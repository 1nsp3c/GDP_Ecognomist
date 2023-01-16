using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dieTime, damage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GnomeMovement gnomeMovement = collision.gameObject.GetComponent<GnomeMovement>();
        Level2Gnome level2Gnome = collision.gameObject.GetComponent<Level2Gnome>();

        if (gnomeMovement != null)
        {
            gnomeMovement.TakeDamage(damage);
            Die();
        }
        if (level2Gnome != null)
        {
            level2Gnome.TakeDamage1(damage);
            Die();
        }

        if (collision.gameObject.tag == "Walls") 
        {
            Die();
        }
        if (collision.gameObject.tag == "Collectables")
        {
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
