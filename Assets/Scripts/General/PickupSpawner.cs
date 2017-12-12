using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {
    public List<Pickup> availablePickups;
    public static PickupSpawner instance;
    public Pickup currentPickup;
    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public Pickup getRandomPickup() {
        return availablePickups[Random.Range(0, availablePickups.Count)];
    }

    public void ResetPickups() {
        Destroy(currentPickup);
    }
    public void spawnPickup(Transform location) {
        if (Random.Range(0f, 1f) > .90f && currentPickup == null) {
            currentPickup = Instantiate(getRandomPickup(), location.position, Quaternion.identity);
        }
    }
}