using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRobots : MonoBehaviour
{
    private float speed = 5.0f;
    private Animator animator;
    private GameManager gamemanager;
    bool to_be_destroyed = false;

    private void Awake()
    {
        
        
        animator = GetComponent<Animator>();
       

        if (animator == null)
        {
            Debug.LogError("Animator component not found in the prefab.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (animator != null)
        {
            //animator.Play("Walk");
            animator.SetBool("Walking2", true);
            
            // Debug.Log(gameObject.name + " is walking!");
           
        }
       

    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            bool isWalking = animator.GetBool("Walking2");
            // Debug.Log(gameObject.name + " is walking: " + isWalking);
        }

        if (!to_be_destroyed)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);

            if (transform.position.z < -20)
            {
                gamemanager.UpdateHealth(1); // subtract 1 from health if a robot gets by the player
                Destroy(gameObject);
                Debug.Log(gameObject.name + " got passed player. Subtracting 1 from health");

            }
        }
    }

    public void setForDestruction()
    {
        to_be_destroyed = true;
    }
}
