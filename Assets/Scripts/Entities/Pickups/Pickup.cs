using UnityEngine;

public abstract class Pickup : MovingObject {

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Action();
            PickupSpawner.instance.currentPickup = null;
            Destroy(gameObject);
        }
    }

    public abstract void Action();
}