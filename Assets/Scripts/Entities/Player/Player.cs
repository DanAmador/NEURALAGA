using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Ships {
    public float thrust = 500f;
    public GameObject bullet;
    public static Player instance;
    private float timeSinceLastShoot = 0;
    private bool isTraining = true;

    public void ResetPlayer() {
        health = 3;
        transform.position = new Vector2(0f, -2f);
        timeSinceLastShoot = 0;
    }

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
        timeSinceLastShoot += Time.deltaTime;
    }


    public override void Damage() {
        base.Damage();
        GameControl.instance.updateHealth();

    }

    protected override void Die() {
        GameControl.instance.updateHealth();
        GameControl.instance.PlayerDied();
        Instantiate(explosion, transform.position, Quaternion.identity);

        if (isTraining) {
            GetComponent<Agent>().done = true;
        }
        else {
            base.Die();
        }
    }

    public void Shoot() {
        if (timeSinceLastShoot > 0.3f) {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeSinceLastShoot = 0;
        }
    }
}