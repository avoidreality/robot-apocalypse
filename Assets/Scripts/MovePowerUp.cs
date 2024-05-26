using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovePowerUp : MonoBehaviour
{
    private float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);

        if (transform.position.z < -15)
        {
            Destroy(gameObject);
        }

    }

   
}
