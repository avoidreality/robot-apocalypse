using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipDeuxExMachina : MonoBehaviour
{
   
    private void Start()
    {
       
      
    }

    private void Update()
    {
       
        transform.Translate(Vector3.back * Time.deltaTime * 1f);

        if (transform.position.z < -15)
        {
            Destroy(gameObject);
        }
        

    }
}
