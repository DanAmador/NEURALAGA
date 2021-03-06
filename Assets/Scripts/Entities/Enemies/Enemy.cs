﻿using System.Collections;
using UnityEngine;

public abstract class Enemy : Ships {

    public float xSpeed, ySpeed, fireRate = 0.03f; //Firerate: seconds between actions
    public bool canShoot;
    public int hitScore;
    [SerializeField]
    private int enemyType;

    public int GetEnemyType() {
        return enemyType;
    }
    
    public void SetEnemyType(int type) {
        this.enemyType = type;
    }
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


    public override void Damage() {
        base.Damage();
        GameControl.instance.UpdateScore(hitScore);
    }

    protected override void Die() {
        PickupSpawner.instance.spawnPickup(transform);
        Spawner.instance.removeEnemy(gameObject);
        base.Die();

    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyBounds") {
            Spawner.instance.removeEnemy(gameObject);
            Destroy(gameObject);
        }
    }
}
