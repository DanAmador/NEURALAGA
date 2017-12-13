using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipAgent : Agent {
    private Player ship;

    private float currentScore = 0;
    private int currentHealth = 3;
    public Text rewardText; 

    public override void InitializeAgent() {
        ship = GetComponent<Player>();
    }
    public override List<float> CollectState() {
        List<float> state = new List<float>();

        state.Add(gameObject.transform.rotation.z);
        state.Add(gameObject.transform.rotation.x);

        for (int i = 0; i < Spawner.instance.maxEnemies; i++) {
            Vector2 pos = Spawner.instance.getEnemyAt(i);
            state.Add(pos.x);
            state.Add(pos.y);
        }
   
        if (PickupSpawner.instance.currentPickup != null) {
            state.Add(PickupSpawner.instance.currentPickup.transform.position.x);
            state.Add(PickupSpawner.instance.currentPickup.transform.position.y);
        }
        else {
            state.Add(0);
            state.Add(0);

        }

        return state;
    }

    public override void AgentStep(float[] act) {


        switch ((int)act[0]) {
            case 0:
            ship.rb.AddForce(new Vector2(ship.thrust, 0));
            break;
            case 1:
            ship.rb.AddForce(new Vector2(-ship.thrust, 0));
            break;
            case 2:
            ship.rb.AddForce(new Vector2(0, ship.thrust));
            break;
            case 3:
            ship.rb.AddForce(new Vector2(0, -ship.thrust));
            break;
            case 4:
            ship.Shoot();
            //reward -= 0.00001f;
            break;

        }
        if (currentScore < (float)GameControl.instance.score) {
            currentScore = (float)GameControl.instance.score;
            reward += 0.05f * currentScore;
        }

        if (currentHealth < GameControl.instance.health) {
            //health kit picked up 
            currentHealth = GameControl.instance.health;
            reward += .1f * currentHealth;
        }
        if (currentHealth > GameControl.instance.health) {
            reward = -1;
            currentHealth = GameControl.instance.health;
        }


        if (GameControl.instance.gameOver) {
            reward = -1;
            done = true;
            return;
        }
        reward += 0.0001f;
        rewardText.text = string.Format("Reward: {0}", CumulativeReward.ToString("0.00"));
    }

    public override void AgentReset() {
        GameControl.instance.ResetScene();
    }

}
