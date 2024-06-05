using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRound : MonoBehaviour
{
    public float speed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (gameObject.transform.position.z > 30)
        {
            Destroy(gameObject);
        }
    }
}