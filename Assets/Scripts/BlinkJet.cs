using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkJet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator BlinkCharacter()
    {
        Renderer cr = GetComponent<Renderer>();
        float elapsedTime = 0f;
        float blinkInterval = 0.1f;
        float total_blink_time = 1.0f;

        while (elapsedTime < total_blink_time)
        {
            cr.enabled = !cr.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        cr.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
