using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    public float timeBetween = 10f;
    public GameObject[] indicatorText;
    // Start is called before the first frame update
    void Start()
    {
        EnemySpawning();
    }

    // Update is called once per frame
    void Update()
    {
    } 
    public void EnemySpawning()
    {
        StartCoroutine(Count());
        StartCoroutine(IndicatorText());
    }

    public IEnumerator Count()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
            // Waits for the time set in timeBetween, affected by timeScale.
            yield return new WaitForSeconds(timeBetween);
        }
    }
    public IEnumerator IndicatorText()
    {
        foreach (GameObject indic in indicatorText)
        {
            indic.SetActive(true);
            yield return new WaitForSeconds(8f);
            indic.SetActive(false);
        }
    }
}
