using UnityEngine;

public class Ships : MovingObject {

    public GameObject explosion;

    protected override void Die() {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    public virtual void Damage() {
        health--;
        if (health <= 0) {
            Die();
        }
    }
}