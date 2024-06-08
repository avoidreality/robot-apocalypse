using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CollisionDetection : MonoBehaviour
{
    public GameObject explosionprefab;
    public GameObject rubblePrefab;
    public GameObject[] fireworksPrefabs;
    public AudioClip player_death;
    public AudioClip robot_death;
    public AudioClip boom6;
    public AudioClip bump;
    public AudioSource AudioSource1;
    public bool game_over = false;
    public SpaceShipDeuxExMachina spaceship;
    public GameObject EscapeRocket;
    private GameManager gamemanager1;
    private BlinkJet jet;
    public GameObject RobotEnemy2;
    private HitCounter hits; 
   
    // Start is called before the first frame update
    void Start()
    {
        if (!game_over)
        {
            GameObject sounds = GameObject.Find("SoundObject");
            if (sounds == null)
            {
                Debug.LogError("SoundObject not found!");
                return;
            }

            AudioSource audioSource = sounds.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource component not found on SoundObject!");
                return;
            }

            AudioSource1 = audioSource;
            Debug.Log("AudioSource successfully found and assigned.");
        } 
        // spaceship = GameObject.Find("Spaceship").GetComponentInChildren<SpaceShipDeuxExMachina>(); does not work
        spaceship = EscapeRocket.GetComponentInChildren<SpaceShipDeuxExMachina>();
        if (spaceship != null)
        {
            Debug.Log("spaceship not null");
        } else
        {
            Debug.LogError(gameObject.name + " Has a null ref for EscapeRocket.");
        }

        gamemanager1 = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gamemanager1.game_over == false) // prevent a null pointer exception when the jet is destroyed but the game is still runnning. 
        {
            jet = GameObject.Find("Fighter_Jet04").GetComponent<BlinkJet>();
        }

        hits = RobotEnemy2.GetComponent<HitCounter>();
     
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Player") && (other.CompareTag("Robot_Alpha") || other.CompareTag("Robot_Beta") || other.CompareTag("Burning_Skull"))) // if an object collides with the player the game is over or now the player loses a point
        {
           Debug.Log("Player hit by " + other.gameObject.name + " !");
            gamemanager1.UpdateHealth(1);
            AudioSource1.PlayOneShot(bump, 1.0f);

            // blink player
            StartCoroutine(jet.BlinkCharacter());

            if (gamemanager1.getHealth() == 0)
            {

                Vector3 playerPosition = gameObject.transform.position;
                Quaternion playerRotation = gameObject.transform.rotation;
                Vector3 offset = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));

                Destroy(gameObject);




                for (int i = 0; i < 4; i++)
                {
                    GameObject fireworks_int = Instantiate(fireworksPrefabs[i], playerPosition + offset, playerRotation);
                    Destroy(fireworks_int, 2);
                    Debug.Log("[+] Firework instance: " + i);


                }
                if (AudioSource1 != null)
                {

                    AudioSource1.PlayOneShot(boom6, 1.0f);
                    AudioSource1.PlayOneShot(player_death, 1.0f);
                    // Debug.Log("Playing 'player death'");
                }
                else
                {
                    Debug.Log("AudioSource1 is null");
                }
                game_over = true;
                gamemanager1.GameOver();
            }
            
            
           



        } else if (gameObject.CompareTag("spaceship"))
        {
            spaceship.missionFail(gameObject.transform);
            Debug.Log("Spaceship collided with " + other.name);
            Destroy(other.gameObject);
            Destroy(gameObject);
            GameObject fireworks_int = Instantiate(fireworksPrefabs[0], gameObject.transform.position, gameObject.transform.rotation);
            GameObject fireworks_int2 = Instantiate(fireworksPrefabs[0], other.gameObject.transform.position, other.gameObject.transform.rotation);

            Destroy(fireworks_int, 2);
            Destroy(fireworks_int2, 2);
            gamemanager1.GameOver();

        }
        /*
       else if (other.CompareTag("Robot_Alpha"))
        {
            GameObject rubble = Instantiate(explosionprefab, gameObject.transform.position, gameObject.transform.rotation);

            AudioSource1.PlayOneShot(robot_death, 1.0f);
            Destroy(other.gameObject);
            Destroy(rubble, 5);
            Debug.Log("Robot_Alpha was hit by: " + gameObject.name);
            
            
           
        }
        else if (other.CompareTag("Robot_Beta"))
        {
            GameObject rubble = Instantiate(explosionprefab, gameObject.transform.position, gameObject.transform.rotation);

            AudioSource1.PlayOneShot(robot_death, 1.0f);
            Destroy(other.gameObject);
            Destroy(rubble, 5);
            Debug.Log("Robot_Beta was hit by: " + gameObject.name);

        }

        */

        if (other.CompareTag("Laser") && gameObject.CompareTag("Robot_Alpha"))
        {
            Debug.Log("Robot_Alpa was hit by: " + other.name);
            //GameObject explosion_inst = Instantiate(explosionprefab, gameObject.transform.position, gameObject.transform.rotation);
            GameObject rubble = Instantiate(fireworksPrefabs[0], gameObject.transform.position, gameObject.transform.rotation);
            Destroy(rubble, 5);
            //Destroy(explosion_inst, 5);
            hits.directHit(); // removes 1 
            if (hits.getHits() == 0)
            {
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
            gamemanager1.UpdateScore(10);

        } else if (other.CompareTag("Laser") && gameObject.CompareTag("Robot_Beta"))
        {
            Debug.Log("Robot_Beta was hit by: " + other.name);
            //GameObject explosion_inst = Instantiate(explosionprefab, gameObject.transform.position, gameObject.transform.rotation);
            GameObject rubble = Instantiate(rubblePrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(rubble, 5);
            //Destroy(explosion_inst, 5);
            Destroy(gameObject);
            gamemanager1.UpdateScore(15);


        }
        else if (other.CompareTag("Laser") && gameObject.CompareTag("Robot_Shooter"))
        {
            Debug.Log("Robot_Shooter was hit by: " + other.name);
            //GameObject explosion_inst = Instantiate(explosionprefab, gameObject.transform.position, gameObject.transform.rotation);
            GameObject rubble = Instantiate(rubblePrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(rubble, 5);
            //Destroy(explosion_inst, 5);
            Destroy(gameObject);
            gamemanager1.UpdateScore(15);


        }
        else if (other.CompareTag("Laser") && gameObject.CompareTag("Burning_Skull")) {

            Debug.Log("Burning Skull was hit by: " + other.name);
            GameObject rubble = Instantiate(fireworksPrefabs[0], gameObject.transform.position, gameObject.transform.rotation);
            Destroy(rubble, 5);
            Destroy(gameObject);
            gamemanager1.UpdateScore(20);

        } else if (other.CompareTag("Robot_Weapon_1") && gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit by robot weapon!");
            AudioSource1.PlayOneShot(bump, 1.0f);

            // blink player
            StartCoroutine(jet.BlinkCharacter());
            gamemanager1.UpdateHealth(1);

            if (gamemanager1.getHealth() == 0)
            {

                Vector3 playerPosition = gameObject.transform.position;
                Quaternion playerRotation = gameObject.transform.rotation;
                Vector3 offset = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));

                Destroy(gameObject);




                for (int i = 0; i < 4; i++)
                {
                    GameObject fireworks_int = Instantiate(fireworksPrefabs[i], playerPosition + offset, playerRotation);
                    Destroy(fireworks_int, 2);
                    Debug.Log("[+] Firework instance: " + i);


                }
                if (AudioSource1 != null)
                {

                    AudioSource1.PlayOneShot(boom6, 1.0f);
                    AudioSource1.PlayOneShot(player_death, 1.0f);
                    // Debug.Log("Playing 'player death'");
                }
                else
                {
                    Debug.Log("AudioSource1 is null");
                }
                game_over = true;
                gamemanager1.GameOver();
            }


        }






    }

    




}
