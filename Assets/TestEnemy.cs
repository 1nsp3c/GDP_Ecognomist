using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    public float timeBetween = 10f;
    // Start is called before the first frame update
    void Start()
    {
        ShowTheCounting();
    }

    // Update is called once per frame
    void Update()
    {
    } 
    public void ShowTheCounting()
    {
        StartCoroutine(Count());
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
}
