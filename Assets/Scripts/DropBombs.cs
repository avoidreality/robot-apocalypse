using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBombs : MonoBehaviour
{
    float speed = 50;
    public GameObject explosion;
    private AudioSource bomb_audio;
    public AudioClip bomb_sound;
    public AudioClip[] hit_sounds;
    private GameManager gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        // Ensure the bomb has an AudioSource component
        bomb_audio = GameObject.Find("SoundObject").GetComponent<AudioSource>();
        if (bomb_audio == null)
        {
            Debug.LogError("No AudioSource found on the SoundObject game object.");
        }

        // Ensure the bomb has a Rigidbody component
        Collider bombCollider = GetComponent<Collider>();
        if (bombCollider == null)
        {
            Debug.LogError("No Collider found on the bomb game object");
        } else if (!bombCollider.isTrigger)
        {
            Debug.LogWarning("Bomb collider is not set as trigger. Setting it to trigger now.");
            bombCollider.isTrigger = true; 
        }

        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bomb collided with something");
        Debug.Log(gameObject + " collided with " + other.gameObject.name);
        if (other.gameObject.name == "Fighter_Jet04")
        {
            return;  // dont' explode yet 
        }
        GameObject big_bang = Instantiate(explosion, transform.position, transform.rotation);
        if (bomb_audio != null)
        {
            bomb_audio.PlayOneShot(bomb_sound, 1f);
        } else
        {
            Debug.LogError("AudioSource for bomb is not assigned.");
        }

        if (other.CompareTag("Robot_Alpha") || other.CompareTag("Robot_Beta") || other.CompareTag("Burning_Skull"))
        {
            gamemanager.UpdateScore(50);
            gamemanager.HitData(other.name);
            Destroy(other.gameObject);
            Debug.Log("Bomb destroyed: " + other.gameObject);
            int index = Random.Range(0, hit_sounds.Length);
            bomb_audio.PlayOneShot(hit_sounds[index], 1f);
        }
        Destroy(big_bang, 5);
        Destroy(gameObject);
    }
}
