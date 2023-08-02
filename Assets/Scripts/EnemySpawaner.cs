using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawaner : MonoBehaviour
{
    public Vector2[] spawnLocation;
    private int enemyCount;
    public GameObject[] enemyPrefab;

    private void Start()
    {
        enemyCount = GameManager.gameManagerInstance.enemyCount;
        while (--enemyCount >= 0)
        {
            var loc = spawnLocation[Random.Range(0, 25) % spawnLocation.Length];
            var obj = Instantiate(enemyPrefab[Random.Range(0,10) % enemyPrefab.Length]);
            obj.transform.position = loc;
        }
    }
}
