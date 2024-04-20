using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
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
           
            Destroy(gameObject);
        }
        else if (other.CompareTag("Robot"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Laser") && gameObject.CompareTag("Robot"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
       

    }
}
