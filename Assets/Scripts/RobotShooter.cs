using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotShooter : MonoBehaviour
{

    public GameObject weapon;
    private AudioSource robot_murderer_sound;
    public AudioClip robot_gunfire;
    private float shootInterval = 1.5f;
    private bool shoot = true;
    private Coroutine shootCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        robot_murderer_sound = GetComponent<AudioSource>();
        shootCoroutine = StartCoroutine(ShootCoroutine());

    }

    // Update is called once per frame
    void Update()
    {



    }

    void Shoot()
    {
        Debug.Log("Robot shooting!");

        if (weapon != null && shoot == true)
        {
            GameObject projectile = Instantiate(weapon, transform.position, weapon.transform.rotation);
        }

        robot_murderer_sound.PlayOneShot(robot_gunfire, 1.0f);

    }

    IEnumerator ShootCoroutine()
    {
        while (shoot)
        {
           
            for (float timer = 0; timer < shootInterval; timer += Time.deltaTime / 2)
            {
                if (!shoot)
                {
                    Debug.Log("Coroutine exiting due to 'shoot' being false");
                    yield break;
                }
                yield return null;
            }
            Shoot();
            yield return new WaitForSeconds(shootInterval);
        }

    }



    private void OnDestroy()
    {
        Debug.Log("OnDestroy called. Stopping shooting.");
        shoot = false;
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
        }
        Debug.Log("Shooting robot obliterated");
        Debug.Log("Setting the weapon to null");
        weapon = null;
    }




}
