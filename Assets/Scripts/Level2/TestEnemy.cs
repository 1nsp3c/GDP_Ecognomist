using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    public float timeBetween = 10f;
    public GameObject[] indicatorText;
    public GameObject firstenemy;
    public GameObject Timer;

    //Timer
    public float startTime;
    public float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.deltaTime;
        StartCoroutine(IndicatorText());
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if ((currentTime - startTime) > 3f && (currentTime - startTime) < 5f)
        {
            EnemySpawning();
        }
    } 
    public void EnemySpawning()
    {
        StartCoroutine(Count());
        
    }

    public IEnumerator Count()
    {
        foreach (GameObject enemy in enemies)
        {
            Debug.Log(enemy.name);
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
