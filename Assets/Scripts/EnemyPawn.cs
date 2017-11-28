using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Enemy {
    private float timeSinceLastAction = 0;

    public GameObject bullet;

    protected override void Action() {
        if (timeSinceLastAction >= fireRate) {
            timeSinceLastAction = 0;
            Shoot();
        }

        timeSinceLastAction += Time.deltaTime;
    }

    void Shoot() {
       GameObject temp =  (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
       temp.GetComponent<Bullet>().ChangeDirection();
    }
}
