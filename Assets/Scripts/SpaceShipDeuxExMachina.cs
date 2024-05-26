using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipDeuxExMachina : MonoBehaviour
{

    public ParticleSystem flamethrower;
    private static bool acquired = false;

    public void setAcquired(bool flag)
    {
        acquired = flag;
        Debug.Log("aquired = " + acquired);
        EscapeEarth();
    }

    private void Start()
    {
        Debug.Log("[+] SpaceShipDeuxExMachina Start() called!");
      
    }

    private void Update()
    {
        Debug.Log("acquired = " + acquired);
        if (acquired == true)
        {
            Debug.Log("Calling EscapeEarth!");
            EscapeEarth();

        }
        else
        {
            Debug.Log("Moving rocket back. acquired = " + acquired);
            transform.Translate(Vector3.up * Time.deltaTime * 5f);

        }

        if (transform.position.z < -15)
        {
            Destroy(gameObject);
        }
    }

    private void EscapeEarth()
    {
        Debug.Log("Escape Earth called. Hello?");
        flamethrower.Play();
        transform.Translate(Vector3.forward * Time.deltaTime * 5f);
    }
}
