using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float firingRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 2f;
    [SerializeField] float minimumFiringRate = 0.2f;

    float baseProjectileSpeed;
    float basefiringRate;
    GameObject baseProjectilePrefab;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    Player player;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        player = gameObject.GetComponent<Player>();
    }
        
    

    void Start()
    {
        if(useAI) //If using AI, have it constantly firing.
        {
            isFiring = true;
        }
        baseProjectileSpeed = projectileSpeed; //Set the baseProjectileSpped to the projectileSpeed at the start of the game.
        basefiringRate = firingRate;
        baseProjectilePrefab = projectilePrefab;
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null) //If is firing and coroutine is not happening.
        {
            firingCoroutine = StartCoroutine(FireContinuously()); //Start firing coroutine.
        }
        else if(!isFiring && firingCoroutine != null) //If not firing and coroutine is happening, stop the coroutine.
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }

    }

    IEnumerator FireContinuously() //Instantiate the projectile at the shooter, and move it downards until its life time has run out.
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifeTime);

            float timeToNextProjectile = UnityEngine.Random.Range(firingRate - firingRateVariance, firingRate + firingRateVariance); //Add variance to when the shooter fires.
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip(); //Play shooting audio clip.
            
            yield return new WaitForSecondsRealtime(timeToNextProjectile);
        }


    }




    public IEnumerator ActivateProjectilePowerUp(GameObject newProjectilePefab, float newProjectileSpeed, float newFiringRate, float powerUpLength) //Change the projectile, projectile speed, and firing rate for a given number of seconds.
    {
        projectilePrefab = newProjectilePefab;
        projectileSpeed = newProjectileSpeed;
        firingRate = newFiringRate;
        yield return new WaitForSecondsRealtime(powerUpLength);
        projectilePrefab = baseProjectilePrefab;
        projectileSpeed = baseProjectileSpeed;
        firingRate = basefiringRate;
        player.PowerUpFinished();

    }
    

}
