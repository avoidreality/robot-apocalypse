using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullLauncher : MonoBehaviour
{
    private int stage_right = -33;
    private int stage_left = 21;
    private int bottom = 1;
    private int top = 3;
    private int startZ = 30; // z position from which the object starts
    public GameObject explosionPrefab;
    private AudioSource skull_radio;
    public AudioClip explosion_sound;
    private GameManager gamemanager;
    bool to_be_destroyed = false;

    // Reference to the Rigidbody component
    private Rigidbody rb;

    // Variable to control the speed of the launch
    private float launchSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the GameObject
        rb = GetComponent<Rigidbody>();

        // Set the initial position of the GameObject
        float randomX = Random.Range(stage_right, stage_left);
        float randomY = Random.Range(bottom, top);
        transform.position = new Vector3(randomX, randomY, startZ);
        skull_radio = GetComponent<AudioSource>();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();


        // Launch the GameObject with a straight vector towards z = 0
        Launch();
    }

    // Method to apply a force along the -z direction
    void Launch()
    {
        // Create a vector that points straight along the -z direction
        Vector3 launchVector = new Vector3(0, 0, -1).normalized * launchSpeed;

        Debug.Log("Burning Skull traveling at this vector: " + launchVector + " with this transform.position: " + transform.position);

        // Apply the force to the Rigidbody
        rb.AddForce(launchVector, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!to_be_destroyed)
        {
            if (transform.position.z < -20)
            {
                Destroy(gameObject);
                gamemanager.UpdateHealth(1); // remove 1 health if the burning skull flies past the player
                Debug.Log(gameObject.name + " got passed player. Subtracting 1 from health");

            }
        }
    }

    // Method to detect trigger collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Debug.Log("SkullLauncher hit by laser!");
            skull_radio.PlayOneShot(explosion_sound, .5f);
            // Implement your logic here, e.g., destroy the skull or apply damage
            Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            gamemanager.HitData("Burning Skull");
            gamemanager.UpdateScore(20);
            Destroy(gameObject);
           
        }
    }

    public void setForDestruction()
    {
        to_be_destroyed = true;
    }
}
