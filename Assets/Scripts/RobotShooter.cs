using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotShooter : MonoBehaviour
{

    public GameObject weapon;
    private AudioSource robot_murderer_sound;
    public AudioClip robot_gunfire;
    private float shootInterval = 2.0f;
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

        GameObject projectile = Instantiate(weapon, transform.position, weapon.transform.rotation);

        robot_murderer_sound.PlayOneShot(robot_gunfire, 1.0f);

    }

    IEnumerator ShootCoroutine()
    {
        while (shoot)
        {
            Shoot();
            for (float timer = 0; timer < shootInterval; timer += Time.deltaTime / 2)
            {
                if (!shoot)
                {
                    Debug.Log("Coroutine exiting due to 'shoot' being false");
                    yield break;
                }
                yield return null;
            }
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
    }




}
