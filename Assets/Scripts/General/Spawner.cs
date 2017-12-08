using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] enemyTypes;
    private float spawnRate, timeSinceLastSpawn;
    [Range(-8, 12)]
    public float minX, maxX;

    private int maxEnemies = 4;

    private void Update() {
        spawnRate = Mathf.Clamp(5 - Mathf.Log((Time.time) / 120 + 1), 0.5f, 10);
        maxEnemies = (int) Mathf.Clamp(Mathf.Exp(Time.time/100f) +1 ,1, 10);

        if (timeSinceLastSpawn >= spawnRate) {
            StartCoroutine("spawnEnemies");
            timeSinceLastSpawn = 0;
        }
        timeSinceLastSpawn += Time.deltaTime;
    }


    IEnumerator spawnEnemies() {
        int amount = Random.Range(1, maxEnemies);

        for (int i = 0; i < amount; i++) {


            Instantiate(enemyTypes[(int)Random.Range(0, enemyTypes.Length)],
                new Vector2(Random.Range(minX, maxX), 7f), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
    void SpawnEnemy() {

    }
}
