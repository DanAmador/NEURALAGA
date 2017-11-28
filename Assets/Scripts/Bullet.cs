using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MovingObject {
    public int direction = 1;

    void Update() {
        rb.velocity = new Vector2(0, 10 * direction);
    }

    public void ChangeDirection() {
        direction *= -1;
    }


    void OnTriggerEnter2D(Collider2D collision) {
        if (direction > 0) {
            if (collision.gameObject.tag == "Enemy") {
                collision.gameObject.GetComponent<Enemy>().Damage();
                this.Die();

            }
        }
        else {
            if (collision.gameObject.tag == "Player") {
                collision.gameObject.GetComponent<Player>().Damage();
                this.Die();
            }

        }
        if (collision.gameObject.tag == "Bounds") {
            this.Die();

        }
    }
}