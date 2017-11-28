using System.Collections;
using UnityEngine;

public abstract class Enemy : MovingObject {

    public float xSpeed,ySpeed, fireRate = 0.03f; //Firerate: seconds between actions
    public bool canShoot;

    
    protected abstract void Action();

    void Start() {
        if (canShoot) {
         InvokeRepeating("Action", fireRate, fireRate);
        }
    }
    protected void Update() {
        rb.velocity = new Vector2(xSpeed, -ySpeed);

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Player>().Damage();
            Die();
        }
    }
}
