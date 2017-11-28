﻿using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {
    public Rigidbody2D rb;
    public int health = 1;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }


    public void Damage() {
        health--;
        if (health <= 0) {
            Die();
        }
    }

    protected void Die() {
        Destroy(gameObject);
    }
}
