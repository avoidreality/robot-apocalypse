using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float speed = 5.0f;
    private Rigidbody playerRb;
    private float xRange = 24;
    public GameObject projectilePrefab;
    private float verticalInput;
    private float zRange = 8.5f;
    

    public Transform projectileSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void KeepPlayerInBoundaries()
    {
        // Keep the player in the boundaries of the game
        float clampedX = Mathf.Clamp(transform.position.x, -xRange, xRange);
        float clampedZ = Mathf.Clamp(transform.position.z, -zRange, zRange);
       

        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
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
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput); // get the xyz coordinates
        playerRb.AddForce(movement * speed, ForceMode.Acceleration); // use this method to move the playe around the screen

        
    }

    void fireWeapon()
    {
        // Fire the food from the player
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Launch a projectile from the player - This currently does not work
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);
            Debug.Log("Spacebar pressed...");

        }

    }

    // Update is called once per frame
    void Update()
    {
        crazyPlayer(); // moves the player
        fireWeapon();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("HealthPowerUp"))
        {
            Debug.Log("Health++");
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("GunPowerUp"))
        {
            Debug.Log("Guns++");
            Destroy(other.gameObject);
        }
    }

   
}
