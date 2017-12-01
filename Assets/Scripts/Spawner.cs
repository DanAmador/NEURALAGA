using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public float rate;
    public GameObject[] enemies;

    void Start () {
        InvokeRepeating("SpawnEnemy", rate, rate);	
	}
	
    void SpawnEnemy() {
        Instantiate(enemies[(int)Random.Range(0, enemies.Length)], 
            new Vector2(Random.Range(-12f, 11.5f), 7f), Quaternion.identity);
    }
}
