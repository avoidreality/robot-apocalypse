using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private int score;
    private int health = 3;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public SpawnManager sm;
    public Button restartButton;
    public GameObject escape_rocket;
    private SpaceShipDeuxExMachina rocket_controls;
    public GameObject ground;
    private ScrollGround ground_control;
    public GameObject titleScreen;
    public bool game_over;

    private float spawn_frequency = 5.5f;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int difficulty)
    {
        Debug.Log("Game Manager is starting the game!");
        UpdateScore(0);
        isGameActive = true;
        spawn_frequency /= difficulty;
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        rocket_controls = escape_rocket.GetComponentInChildren<SpaceShipDeuxExMachina>();
        ground_control = ground.GetComponent<ScrollGround>();
        StartCoroutine(sm.SpawnGameObjects(spawn_frequency));
        StartCoroutine(sm.InstantiateSpaceshipAfterDelay());
        titleScreen.gameObject.SetActive(false);
        DisplayHealth();
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public int getHealth()
    {
        return health;
    }

    public void DisplayHealth()
    {
        healthText.text = "Health: " + health;
    }

    public void UpdateHealth(int minusHealth)
    {
        health -= minusHealth;
        healthText.text = "Health: " + health;

        if (health < 0)
        {
            Debug.Log("Player ran out of health. Bad health. Game Over");
            GameOver();
        }
        
    }

    public void AddHealth()
    {
        Debug.Log("Adding 1 health point");
        health += 1;
        healthText.text = "Health: " + health;

    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        sm.setRocketInst(false);
        restartButton.gameObject.SetActive(true);
        rocket_controls.setAcquired(false);
        game_over = true;
    }

    public void RestartGame()
    {
        Debug.Log("Restart Button pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
