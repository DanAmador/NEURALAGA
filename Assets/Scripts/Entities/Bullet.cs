using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MovingObject {
    public int direction = 1;

    void Update() {rb.velocity = new Vector2(0, direction);}
    public void ChangeDirection() {direction *= -1;}
    public void ChangeColor(Color col) {GetComponent<SpriteRenderer>().color = col;}

    void OnTriggerEnter2D(Collider2D collision) {
        if (direction > 0) {
            if (collision.gameObject.tag == "Enemy") {
                collision.gameObject.GetComponent<Enemy>().Damage();
                Destroy(gameObject);
            }
        }
        else {
            if (collision.gameObject.tag == "Player") {
                collision.gameObject.GetComponent<Player>().Damage();
                Destroy(gameObject);
            }

        }
        if (collision.gameObject.tag == "Bounds") {
            Destroy(gameObject);
        }
    }
}