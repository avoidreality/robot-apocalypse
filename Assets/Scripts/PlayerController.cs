using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float forwardInput;
    private float speed = 10.0f;
    private Rigidbody playerRb;
    private float stage_right_xRange = 22;
    private float stage_left_xRange = -34;
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

    
    float fireRate = 0.1f;
    private float nextFireTime = 0.0f;

    public ParticleSystem fire;

    private Vector2 moveInput; // For the touch-screen controls
    private VirtualJoystick virtualJoystick; // For the touch-screen controls
    private GameInputActions inputActions; // For the touch-screen controls
    private bool isFiring = false; // for the touch-screen 'fire' button
   
    

    // For the touch-screen controls
    private void Awake()
    {
        inputActions = new GameInputActions();
        virtualJoystick = FindObjectOfType<VirtualJoystick>();
        playerRb = GetComponent<Rigidbody>();
    }

    // For the touch-screen controls
    private void OnEnable()
    {
        inputActions.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Gameplay.Fire.performed += ctx => Fire();
        inputActions.Gameplay.Fire.canceled += ctx => StopFiring();
        inputActions.Gameplay.Fly.performed += ctx => Flying();
        inputActions.Gameplay.Descent.performed += ctx => Descending();
        inputActions.Gameplay.Main.performed += ctx => switchToMainGun();
       
        inputActions.Enable();
    }

    // For the touch-screen controls
    private void OnDisable()
    {
        inputActions.Gameplay.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Gameplay.Move.canceled -= ctx => moveInput = Vector2.zero;
        inputActions.Gameplay.Fire.performed -= ctx => Fire();
        inputActions.Gameplay.Fire.canceled -= ctx => StopFiring();
        
       
        inputActions.Disable();
    }

    // for touch-screen
    private void switchToMainGun()
    {
        Debug.Log("Touch screen switching to main gun...");
        gun_number = 0;
        current_gun = projectilePrefab[gun_number];
    }

    private void Flying()
    {
        Debug.Log("On screen button flying pressed");
        transform.Translate(Vector3.up * Time.deltaTime * 50);
        fire.Play();
        
    }

    private void Descending()
    {
        Debug.Log("On screen Descent Button pressed");
        transform.Translate(Vector3.down * Time.deltaTime * 50);
        fire.Stop();
    }

    private void Fire()
    {
        if (player_gameManager.isGameActive)
        {
            Debug.Log("Fire Button Pressed");
            isFiring = true;

            // Launch a projectile from the player - This currently does not work
            Instantiate(current_gun, projectileSpawnPoint.position, current_gun.transform.rotation);

            sounds.PlayOneShot(gun_sound, 1.0f);
            
        }
    }

    private void StopFiring()
    {
        isFiring = false;
    }

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
        float clampedX = Mathf.Clamp(transform.position.x, stage_left_xRange, stage_right_xRange);
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
        if (playerRb != null)
        {
            // Get joystick input
            Vector2 joystickInput = virtualJoystick.GetInput();

            // Get AD keys or left and right arrow keys - keyboard input
            float horizontalInput = Input.GetAxis("Horizontal");

            // Get WS keys or up and down arrow keys - keyboard input
            float forwardInput = Input.GetAxis("Vertical");

            // Combine keyboard and joystic inputs
            Vector3 combinedInput = new Vector3(horizontalInput + joystickInput.x, 0, forwardInput + joystickInput.y);
            playerRb.AddForce(combinedInput * speed, ForceMode.Acceleration); // Move the player with the combined inputs

            // Previous way to move the player with just keyboard input
            //Vector3 movement = new Vector3(horizontalInput, gameObject.transform.position.y, forwardInput); // get the xyz coordinates
            //playerRb.AddForce(movement * speed, ForceMode.Acceleration); // use this method to move the player around the screen
        }

        
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
                Debug.Log("Spacebar pressed...firing!");
            }
        }
        
    }

    private IEnumerator FireContinuously()
    {
       

        while ((Input.GetKey(KeyCode.Space) || isFiring) && player_gameManager.isGameActive && current_gun.name == "FieldGunRound_01")
        {
            Instantiate(current_gun, projectileSpawnPoint.position, current_gun.transform.rotation);
            sounds.PlayOneShot(gun_sound, 1.0f);
            yield return new WaitForSeconds(fireRate);
        }

      
    }

    void fly()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Flying...");
            transform.Translate(Vector3.up * Time.deltaTime * 50);
            fire.Play();
        }
      
    }

    void mainGun()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Switching back to main gun...");
            gun_number = 0;
            current_gun = projectilePrefab[gun_number];
        }
    }

    void ground()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Flying towards ground");
            transform.Translate(Vector3.down * Time.deltaTime * 50);
            fire.Stop();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (player_gameManager.isGameActive && current_gun.name == "FieldGunRound_01")
        {
            if ((Input.GetKey(KeyCode.Space) || isFiring) && Time.time > nextFireTime && current_gun.name == "FieldGunRound_01")
            {
                nextFireTime = Time.time + fireRate;
                StartCoroutine(FireContinuously());
                Debug.Log("Machine gun activated!");
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                StopCoroutine(FireContinuously());
            }
        }
        if (playerRb != null)
        {
            crazyPlayer(); // moves the player
            fireWeapon(current_gun);
            fly();
            ground();
            mainGun();
        }
        
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

    public void explodePlayer()
    {
        if (gameObject != null)
        {
            Vector3 playerPosition = gameObject.transform.position;
            Quaternion playerRotation = gameObject.transform.rotation;
            Vector3 offset = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
            Destroy(gameObject);



            for (int i = 0; i < 4; i++)
            {
                GameObject fireworks_int = Instantiate(fireworks[i], playerPosition + offset, playerRotation);
                Destroy(fireworks_int, 5);
                Debug.Log("[+] Firework instance: " + i);
                sounds.PlayOneShot(gun_sound, 1f);
                sounds.PlayOneShot(death, 1f);


            }
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
            if (gun_number >= projectilePrefab.Length) {
                gun_number = projectilePrefab.Length - 1; // stay at the last gun until player obliteration 
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
            player_gameManager.UpdateScore(5);
            

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
           
            explodePlayer();

        }



    }


}
