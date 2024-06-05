using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipDeuxExMachina : MonoBehaviour
{

    public ParticleSystem flamethrower;
    private static bool acquired = false;
    public AudioSource rocket_audio;
    public AudioClip rocket_launch_sound;
    private PlayerController playercontroller;

    public GameObject explosion;

    public CollisionDetection collisions;
   
   
    public void setAcquired(bool flag)
    {
        acquired = flag;
        Debug.Log("aquired = " + acquired);
        
        //EscapeEarth();
        
    }

    

    private void Start()
    {
        collisions = GetComponent<CollisionDetection>();
        Debug.Log("collisions = " + collisions);
    }

    private void Update()
    {
       // Debug.Log("acquired = " + acquired);
        if (acquired == true)
        {
            //Debug.Log("Calling EscapeEarth!");
            EscapeEarth();

        }
        else
        {
           // Debug.Log("Moving rocket back. acquired = " + acquired);
            transform.Translate(Vector3.up * Time.deltaTime * 5f);

        }

        if (transform.position.z < -15)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 50)
        {
            Destroy(gameObject);
        }
    }

    private void EscapeEarth()
    {
        //Debug.Log("Escape Earth called. Hello?");
        flamethrower.Play();
        transform.Translate(Vector3.forward * Time.deltaTime * 5f);

    }

    public void missionFail(Transform obstacle_transform)
    {
        
        GameObject fireworks_int = Instantiate(explosion, obstacle_transform.position, obstacle_transform.rotation);
        Destroy(fireworks_int, 2);
        Destroy(gameObject);
        Debug.Log("Spaceship crashed. Escape mission failure.");
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3 offset = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
                GameObject fireworks_int = Instantiate(explosion, gameObject.transform.position + offset, gameObject.transform.rotation);
                Destroy(fireworks_int, 2);
                Destroy(gameObject);
               
                Debug.Log("[+] OnCollisionEnter - Spaceship crashed. Escape mission failure.");

            }
        }
    }
    

   
}
