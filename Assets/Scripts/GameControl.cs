using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameControl : MonoBehaviour {
    public static GameControl instance;
    public bool gameOver = false;
    private int score, health = 3;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void UpdateScore(int score) {
        if (!gameOver) {
            this.score += score ;
            scoreText.text = "Score: " + this.score.ToString();
        }
    }

    public void updateHealth(int health) {
        if (!gameOver) {
            this.health = health;
            healthText.text = "Health: " + this.health.ToString();
        }
    }
    public void PlayerDied() {
        gameOver = true;
    }
}