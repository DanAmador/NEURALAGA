using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {
    public float thrust = 500f;
    public GameObject bullet;


    void Awake() {
        health = 3;
        rb = GetComponent<Rigidbody2D>();
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

    void Shoot() {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}