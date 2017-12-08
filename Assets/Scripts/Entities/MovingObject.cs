using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {
    public Rigidbody2D rb;
    public int health = 1;
    void Awake() {
        rb = GetComponent<Rigidbody2D>();   
    }


    public virtual void Damage() {
        health--;
        if (health <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }
}
