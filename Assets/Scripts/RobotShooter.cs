using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotShooter : MonoBehaviour
{

    public GameObject weapon;
    private AudioSource robot_murderer_sound;
    public AudioClip robot_gunfire;
    public float shootInterval = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        robot_murderer_sound = GetComponent<AudioSource>();
        
        InvokeRepeating("Shoot", 0f, shootInterval);
    }

    // Update is called once per frame
    void Update()
    {

      

    }

    void Shoot()
    {
        Debug.Log("Robot shooting!");
        // Launch a projectile from the player - This currently does not work
        Instantiate(weapon, transform.position, weapon.transform.rotation);

        robot_murderer_sound.PlayOneShot(robot_gunfire, 1.0f);


    }
}
