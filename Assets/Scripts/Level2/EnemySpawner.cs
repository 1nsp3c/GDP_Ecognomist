using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnInterval = 1f;
    public GameObject spawner;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
        
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) 
    { 
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, spawner.transform.position, Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
    
}
