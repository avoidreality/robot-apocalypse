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
    public TextMeshProUGUI hitText;
    public bool isGameActive;
    private SpawnManager spawn_manager;
    public GameObject escape_rocket;
    private SpaceShipDeuxExMachina rocket_controls;
    public GameObject ground;
    private ScrollGround ground_control;
    public GameObject titleScreen;
    public bool game_over;
    public GameObject WinScreen;
    public GameObject TextFields;
    public GameObject GameOverFields;

    private AudioSource game_manager_radio;
    public AudioClip cyborg_congratulates_player;

    public PlayerController player;

    private float spawn_frequency = 5.5f;

    public Toggle onScreenControlsToggle; // Reference to the Toggle UI element
    public GameObject onScreenControlsCanvas; // Reference to the on-screen controls Canvas 

    // Start is called before the first frame update
    void Start()
    {
        game_manager_radio = GameObject.Find("SoundObject").GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        // Initialize the on-screen controls based on the toggle's value
        onScreenControlsCanvas.SetActive(onScreenControlsToggle.isOn);

        // Add listener to the toggle
        onScreenControlsToggle.onValueChanged.AddListener(delegate
        {
            ToggleOnScreenControls(onScreenControlsToggle);
        });
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
        spawn_manager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        rocket_controls = escape_rocket.GetComponentInChildren<SpaceShipDeuxExMachina>();
        ground_control = ground.GetComponent<ScrollGround>();
        StartCoroutine(spawn_manager.SpawnGameObjects(spawn_frequency));
        StartCoroutine(spawn_manager.InstantiateSpaceshipAfterDelay());
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
        if (health > 0)
        {
            health -= minusHealth;
        }

        healthText.text = "Health: " + health;

        if (health == 0)
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

    public void HitData(string name)
    {
        Debug.Log("[+] Got this hit data: " + name);
        hitText.text = "Hit: " + name;
        StartCoroutine(clearHitData(1f));
    }

    IEnumerator clearHitData(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        hitText.text = "Hit: ";
    }

    public void GameOver()
    {

        GameOverFields.SetActive(true);
        isGameActive = false;
        spawn_manager.setRocketInst(false);
        rocket_controls.setAcquired(false);
        game_over = true;
        if (player != null)
        {
            player.explodePlayer();
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restart Button pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Winner()
    {
        WinScreen.SetActive(true);
        game_over = true;
        spawn_manager.setRocketInst(false);
        isGameActive = false;
        rocket_controls.setAcquired(false);
        TextFields.SetActive(false);
        game_manager_radio.PlayOneShot(cyborg_congratulates_player, 1f);
    }

    private void ToggleOnScreenControls(Toggle toggle)
    {
        // Activate or deactivate the on-screen controls canvas based on the toggle's value
        onScreenControlsCanvas.SetActive(toggle.isOn);
    }
}
