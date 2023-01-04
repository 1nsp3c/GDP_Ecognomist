using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProj : MonoBehaviour
{
    public float dieTime, damage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Die();
        }
        if (collision.gameObject.tag == "Walls")
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
