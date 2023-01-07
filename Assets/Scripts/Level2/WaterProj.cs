using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProj : MonoBehaviour
{
    public float dieTime, damage;
    Level2Gnome level2Gnome;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemySecondLevel enemy = collision.gameObject.GetComponent<EnemySecondLevel>();
        if (enemy != null)
        {
            enemy.TakeDamageFire(damage);
            Die();
        }
        if (collision.gameObject.tag == "Walls")
        {
            Die();
        }
        
        //Collide with Tree
        if (collision.gameObject.layer == 9)
        {
            Tree treeScript = collision.gameObject.GetComponent<Tree>();
            treeScript.timesHit += 1;
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
