﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
    public static GameControl instance;
    public bool gameOver = false;
    public int score, health = 3;
    public Text scoreText, healthText;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(this.gameObject);
        }
    }


    void Update() {
        if (gameOver && Input.GetMouseButtonDown(0)) {
            ResetScene();
        }
    }

    public void ResetScene() {
        gameOver = false;
        Spawner.instance.ResetEnemies();
        PickupSpawner.instance.ResetPickups();
        Player.instance.ResetPlayer();
        score = 0;
        updateHealth();
        UpdateScore(score);
    }
    public void UpdateScore(int score) {
        if (!gameOver) {
            this.score += score ;
            scoreText.text = "Score: " + this.score.ToString();
        }
    }

    public void updateHealth() {
        if (!gameOver) {
            this.health = Player.instance.health;
            healthText.text = "Health: " + this.health.ToString();
        }
    }

    public void PlayerDied() {
        gameOver = true;
        ResetScene();
    }
}