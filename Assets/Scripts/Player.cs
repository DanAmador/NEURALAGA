using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {
    public float thrust = 500f;
    public GameObject bullet;
    public GameObject explosion;
    public static Player instance;

    void Awake() {
        health = 3;
        rb = GetComponent<Rigidbody2D>();

        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(0, 0);
        float x = Input.GetAxis("Horizontal") * thrust;
        float y = Input.GetAxis("Vertical") * thrust;
        rb.AddForce(new Vector2(x, y));

        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }
    }


    public void Damage() {
        health--;
        GameControl.instance.updateHealth(health);
        if (health <= 0) {
            Die();
            GameControl.instance.PlayerDied();
        }
    }

    protected override void Die() {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    void Shoot() {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}