using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Ships {
    public float thrust = 500f;
    public GameObject bullet;
    public static Player instance;

    void Awake() {
        health = 3;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
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


    public override void Damage() {
        base.Damage();
        GameControl.instance.updateHealth();

    }

    protected override void Die() {
        GameControl.instance.PlayerDied();
        base.Die();

    }

    void Shoot() {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}