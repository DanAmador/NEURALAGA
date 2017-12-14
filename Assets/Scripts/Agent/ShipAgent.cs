using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipAgent : Agent {
    private Player ship;

    private float currentScore = 0;
    private int currentHealth = 3;
    private int currenOnScreenEnemies = 0;
    public Text rewardText;

    public override void InitializeAgent() {
        ship = GetComponent<Player>();
    }
    public override List<float> CollectState() {
        List<float> state = new List<float>();

        state.Add(gameObject.transform.rotation.z);
        state.Add(gameObject.transform.rotation.x);

        for (int i = 0; i < Spawner.instance.maxEnemies; i++) {
            ArrayList enemyValues = Spawner.instance.getEnemyAt(i);
            foreach (float value in enemyValues) {
                state.Add(value);
            }
        }

        if (PickupSpawner.instance.currentPickup != null) {
            state.Add(1);
            state.Add(PickupSpawner.instance.currentPickup.transform.position.x);
            state.Add(PickupSpawner.instance.currentPickup.transform.position.y);
        }
        else {
            state.Add(0);
            state.Add(0);
            state.Add(0);

        }

        return state;
    }

    public override void AgentStep(float[] act) {


        switch ((int)act[0]) {
            case 0:
            ship.rb.AddForce(new Vector2(ship.thrust, 0));
            reward += 0.01f;
            break;
            case 1:
            ship.rb.AddForce(new Vector2(-ship.thrust, 0));
            reward += 0.01f;
            break;
            case 2:
            ship.rb.AddForce(new Vector2(0, ship.thrust));
            reward -= 0.005f;
            break;
            case 3:
            ship.rb.AddForce(new Vector2(0, -ship.thrust));
            reward += 0.01f;
            break;
            case 4:
            ship.Shoot();
            reward += 0.05f;
            break;

        }

        if (currentScore < (float)GameControl.instance.score) {
            //Enemy killed
            currentScore = (float)GameControl.instance.score;
            currenOnScreenEnemies = Spawner.instance.getOnScreenEnemies();
            reward += 1f;
        }

        if (currentHealth < GameControl.instance.health) {
            //health kit picked up 
            currentHealth = GameControl.instance.health;
            reward += 1f;
        }
        if (currentHealth > GameControl.instance.health) {
            reward -= .5f;
            currentHealth = GameControl.instance.health;
        }


        if (GameControl.instance.gameOver) {
            reward = -1;
            done = true;
            return;
        }
        reward = Mathf.Clamp(reward, -1f, 1f);
        rewardText.text = string.Format("Reward: {0}", CumulativeReward.ToString("0.00"));
    }

    public override void AgentReset() {
        GameControl.instance.ResetScene();
    }

}
