using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipDeuxExMachina : MonoBehaviour
{
   
    public GameObject escapeRocket; // Reference to the prefab you want to instantiate
    public float delayInSeconds = 30f; // Delay before instantiation

    private void Start()
    {
        StartCoroutine(InstantiateObjectAfterDelay());
    }

    IEnumerator InstantiateObjectAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        // Instantiate the object after the delay
        Vector3 pos = new Vector3(-2.5f, 5f, 5f);
        Instantiate(escapeRocket, pos, Quaternion.identity);
    }
}
