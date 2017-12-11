using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] enemyTypes;
    private float spawnRate, timeSinceLastSpawn;
    public int maxEnemies = 10;

    [Range(-8, 12)]
    public float minX, maxX;
    [SerializeField]
    private List<GameObject> enemiesSpawned;
    public static Spawner instance;

    void Awake() {
        enemiesSpawned = new List<GameObject>();
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this.gameObject);
        }
    }

    public Vector3 getEnemyAt(int index) {
        if(index > getAmount()-1) {
            return new Vector2(0, 0);
        }
        else {
            return (Vector2)enemiesSpawned[index].transform.position;
        }
    }
    public int getAmount() {
        return enemiesSpawned.Count;
    }

    public void removeEnemy(GameObject toRemove) {
        enemiesSpawned.Remove(toRemove);
    }

    public void addEnemy(GameObject toAdd) {
        enemiesSpawned.Add(toAdd);
    }




    private void Update() {
        spawnRate = Mathf.Clamp(5 - Mathf.Log((Time.time) / 120 + 1), 0.5f, 10);
        maxEnemies = (int)Mathf.Clamp(Mathf.Exp(Time.time / 100f) + 1, 1, 10);

        if (timeSinceLastSpawn >= spawnRate) {
            StartCoroutine("spawnEnemies");
            timeSinceLastSpawn = 0;
        }
        timeSinceLastSpawn += Time.deltaTime;
    }


    IEnumerator spawnEnemies() {
        int amount = Random.Range(1, maxEnemies);

        if (getAmount() < maxEnemies) {
            for (int i = 0; i < amount; i++) {
                addEnemy(Instantiate(enemyTypes[(int)Random.Range(0, enemyTypes.Length)],
                    new Vector2(Random.Range(minX, maxX), 7f), Quaternion.identity));
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
