using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] enemyTypes;
    [SerializeField]
    private float spawnRate, timeSinceLastSpawn, onScreenEnemies, timePassed;
    public int maxEnemies = 15;
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
        if (index > enemiesSpawned.Count - 1) {
            return new Vector2(0, 0);
        }
        else {
            if (!enemiesSpawned[index].GetComponent<Renderer>().isVisible) {
                return (Vector2)enemiesSpawned[index].transform.position;
            }
            else {
                return new Vector2(0, 0);

            }
        }
    }


    public void removeEnemy(GameObject toRemove) {
        enemiesSpawned.Remove(toRemove);
    }

    public void addEnemy(GameObject toAdd) {
        enemiesSpawned.Add(toAdd);
    }

    public void ResetEnemies() {
        timePassed = 0;
        timeSinceLastSpawn = 0;

        foreach (GameObject e in enemiesSpawned) {
            Destroy(e);
        }

        enemiesSpawned.RemoveAll(delegate (GameObject o) { return o == null; });
        enemiesSpawned.Clear();
    }


    void Update() {
        spawnRate = Mathf.Clamp(5 - Mathf.Log((timePassed) / 120 + 1), 0.5f, 10);
        onScreenEnemies = (int)Mathf.Clamp(Mathf.Exp(timePassed / 50f) + 1, 1, maxEnemies);

        if (timeSinceLastSpawn >= spawnRate) {
            StartCoroutine("spawnEnemies");
            timeSinceLastSpawn = 0;
        }
        timeSinceLastSpawn += Time.deltaTime;
        timePassed += Time.deltaTime;

    }


    IEnumerator spawnEnemies() {
        int waveAmount = Random.Range(1, maxEnemies);

        for (int i = 0; i < waveAmount; i++) {
            if (enemiesSpawned.Count < onScreenEnemies) {

                addEnemy(Instantiate(enemyTypes[(int)Random.Range(0, enemyTypes.Length)],
                    new Vector2(Random.Range(minX, maxX), 7f), Quaternion.identity));
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
