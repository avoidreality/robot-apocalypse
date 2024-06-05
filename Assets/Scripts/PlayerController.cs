using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float forwardInput;
    private float speed = 5.0f;
    private Rigidbody playerRb;
    private float xRange = 24;
    public GameObject[] projectilePrefab;
    private int gun_number = 0;
    private GameObject current_gun;
    private float verticalInput;
    private float zBottomRange = -13.5f;
    private float zTopRange = 1.5f;
    private float yBottomRange = 0.5f;
    private float yTopRange = 10.0f;

    //public Material initialMaterial; // Initial material of the floor
    // public Material alternateMaterial; // Material to switch to
    public Material initialMaterial;
    public Material alternateMaterial;
    private bool isAlternate = false; // track current floor material state 

    public GameObject spaceship;
    private SpaceShipDeuxExMachina spaceShipDeuxExMachina;

    public Transform projectileSpawnPoint;

    public AudioSource sounds;
    public AudioClip death;
    public AudioClip gun_sound;
    public AudioClip warp_sound;
    public AudioClip gun_power_up;
    public AudioClip health_power_up;
    public AudioClip rocket_launch_sound;
    public AudioClip liftoff;
    public GameObject[] fireworks;
    private CollisionDetection collisions;
    private GameManager player_gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        spaceShipDeuxExMachina = spaceship.GetComponentInChildren<SpaceShipDeuxExMachina>();
        current_gun = projectilePrefab[0];

        sounds = GameObject.Find("SoundObject").GetComponent<AudioSource>();

        collisions = GameObject.Find("Player").GetComponent<CollisionDetection>();

        player_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        
    }

    void KeepPlayerInBoundaries()
    {
        // Keep the player in the boundaries of the game
        float clampedX = Mathf.Clamp(transform.position.x, -xRange, xRange);
        float clampedZ = Mathf.Clamp(transform.position.z, zBottomRange, zTopRange);
        float clampedY = Mathf.Clamp(transform.position.y, yBottomRange, yTopRange);
       

        transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }

    void FixedUpdate()
    {
        KeepPlayerInBoundaries();
    }



    void crazyPlayer()
    {
        // Get AD keys or left and right arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");

        // Get WS keys or up and down arrow keys
        float forwardInput = Input.GetAxis("Vertical");

        

        Vector3 movement = new Vector3(horizontalInput, gameObject.transform.position.y, forwardInput); // get the xyz coordinates
        playerRb.AddForce(movement * speed, ForceMode.Acceleration); // use this method to move the player around the screen

        
    }

    void fireWeapon(GameObject gun_type)
    {
        if (player_gameManager.isGameActive)
        {
            // Fire the laser from the player
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Launch a projectile from the player - This currently does not work
                Instantiate(gun_type, projectileSpawnPoint.position, gun_type.transform.rotation);

                sounds.PlayOneShot(gun_sound, 1.0f);
                //Debug.Log("Spacebar pressed...");
            }
        }
        
    }

    void fly()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Flying...");
            transform.Translate(Vector3.up * Time.deltaTime * 50);
        }
      
    }

    void ground()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Flying towards ground");
            transform.Translate(Vector3.down * Time.deltaTime * 50);
        }

    }

    // Update is called once per frame
    void Update()
    {
        crazyPlayer(); // moves the player
        fireWeapon(current_gun);
        fly();
        ground();
        
    }

    public void rocket_launch()
    {
        Debug.Log("Rocket Launch audio started!");

        if (sounds.isActiveAndEnabled)
        {
            
           
                sounds.PlayOneShot(rocket_launch_sound, .5f);
                sounds.PlayOneShot(liftoff, 1f);



        }
        else
        {
            Debug.LogError("rocket_audio not active or enabled");

        }

    }

   

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("HealthPowerUp"))
        {
            Debug.Log("Health++");
            Destroy(other.gameObject);
            sounds.PlayOneShot(health_power_up, 1.0f);
            player_gameManager.AddHealth();
        }

        if (other.gameObject.CompareTag("GunPowerUp"))
        {
            Debug.Log("Guns++");
            Destroy(other.gameObject);
            sounds.PlayOneShot(gun_power_up, 1.0f);
            gun_number += 1;
            if (gun_number > 2) {
                gun_number = 2; // stay at the last gun until player obliteration 
            }
            current_gun = projectilePrefab[gun_number];
            Debug.Log("current_gun = " + current_gun);
            
        } 

        if (other.gameObject.CompareTag("Warp"))
        {
            Debug.Log("Warp found");
            Destroy(other.gameObject); // Destroy the warp 
            Debug.Log("Player found the warp!");
            sounds.PlayOneShot(warp_sound, 0.5f);
            

            // Toggle between materials
            GameObject groundObject = GameObject.FindWithTag("ground");
            Renderer floorRenderer = groundObject.GetComponent<MeshRenderer>();

            if (!isAlternate)
            {
                // Change to alternate material
                floorRenderer.material = alternateMaterial;
                isAlternate = true;
            }
            else
            {
                floorRenderer.material = initialMaterial;
                isAlternate = false;
            }
        }

        if (other.gameObject.CompareTag("spaceship") && !collisions.game_over)
        {
            Debug.Log("Spaceship found!");
            //Destroy(other.gameObject);
            spaceShipDeuxExMachina.setAcquired(true);
            Debug.Log("Player wins!");
            collisions.game_over = true; // the player won
            rocket_launch();
        }

       /*if (other.gameObject.CompareTag("spaceship") && collisions.game_over)
        {
            Debug.Log("Escape rocket ran into player!");
            Destroy(other.gameObject);
            Destroy(gameObject);

            Vector3 playerPosition = gameObject.transform.position;
            Quaternion playerRotation = gameObject.transform.rotation;
            for (int i = 0; i < 4; i++)
            {
                GameObject fireworks_int = Instantiate(fireworks[i],  playerPosition, playerRotation);
                Destroy(fireworks_int, 5);
                Debug.Log("[+] Firework instance: " + i);


            }



        }
       */

        if (other.gameObject.CompareTag("Robot")) // This robot collision code does not execute. I don't know why exactly.
        {
            Debug.Log("PlayerController hit!");
            if (sounds != null)
            {
                sounds.PlayOneShot(death, 1.0f);
                Debug.Log("Playing 'player death'");
                
            }
            else
            {
                Debug.Log("'sounds' is null");
            }
            Vector3 playerPosition = gameObject.transform.position;
            Quaternion playerRotation = gameObject.transform.rotation;
            Vector3 offset = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
            Destroy(gameObject);



            for (int i = 0; i < 4; i++)
            {
                GameObject fireworks_int = Instantiate(fireworks[i], playerPosition + offset, playerRotation);
                Destroy(fireworks_int, 5);
                Debug.Log("[+] Firework instance: " + i);


            }
        }



    }


}
