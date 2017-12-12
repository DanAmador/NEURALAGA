using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikaze : Enemy {
    private Vector2 _startPosition, axis;
    public float frequency = 5f;
    public GameObject kamiKill;

    protected override void Action() {

        Instantiate(kamiKill, transform.position, Quaternion.identity);
        Spawner.instance.removeEnemy(gameObject)
        Destroy(gameObject);
    }

    void Start() {
        xSpeed = Random.Range(1f, 5f); //magnitude
        ySpeed = Random.Range(2f, 3f);
        _startPosition = transform.position;
        axis = transform.right;
    }


    void FixedUpdate() {
        _startPosition += (Vector2)transform.up * Time.deltaTime * ySpeed * -1;
        transform.position = _startPosition - axis * Mathf.Sin(Time.time * frequency) * xSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyBounds") {
            Spawner.instance.removeEnemy(gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Player>().Damage();
            Action();
        }
    }
}
