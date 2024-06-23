using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCounter : MonoBehaviour
{
    private int total_hits  = 3; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int getHits()
    {
        return total_hits;
    }

    public void directHit()
    {
        total_hits -= 1;
        Debug.Log("Direct hit subtracted 1 from total_hits! total_hits: " + total_hits);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
