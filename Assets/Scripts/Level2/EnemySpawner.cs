using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnInterval = 1.5f;
    public GameObject spawner;
    private int spawnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) 
    {
        while (spawnCount < 2) 
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, spawner.transform.position, Quaternion.identity);
            spawnCount += 1;
        }
        
    }
    
}
