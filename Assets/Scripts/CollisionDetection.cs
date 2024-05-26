using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CollisionDetection : MonoBehaviour
{
    public GameObject explosionprefab;

    public GameObject rubblePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // if an object collides with the player the game is over or now the player loses a point
        {
           
            
            GameObject rubble = Instantiate(rubblePrefab, gameObject.transform.position, gameObject.transform.rotation);
            
            
            Destroy(other.gameObject);
            Destroy(rubble, 5);
            

        }
        else if (other.CompareTag("Robot"))
        {
           
            GameObject explosion_inst = Instantiate(explosionprefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
            Destroy(explosion_inst, 5);
        }

        if (other.CompareTag("Laser") && gameObject.CompareTag("Robot"))
        {
            
            //GameObject explosion_inst = Instantiate(explosionprefab, gameObject.transform.position, gameObject.transform.rotation);
            GameObject rubble = Instantiate(rubblePrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(rubble, 5);
            //Destroy(explosion_inst, 5);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
       

    }
}
