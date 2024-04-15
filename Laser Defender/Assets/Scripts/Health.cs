using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;

    [SerializeField] private int health = 50; 

    [SerializeField] int scoreIncrease = 100;

    [SerializeField] bool applyCameraShake;

    [SerializeField] ParticleSystem hitEffect;

    [Header("Power Ups")]
    [SerializeField] bool spawnsPowerUpOnDeath;
    [SerializeField] List<GameObject> powerups;
    [SerializeField] float powerUpSpeed = 5f;
    [SerializeField] float powerUpLifeTime = 5f;
    PowerUpSpawner powerUpSpawner;
    [SerializeField] bool immuneToDamage;
    int maxHealth;



    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    SpriteRenderer spriteRenderer;
    Player player;

    void Awake() 
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        powerUpSpawner = FindObjectOfType<PowerUpSpawner>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        player = gameObject.GetComponent<Player>();
    }

    void Start()
    {
        maxHealth = health;
    }


    void OnTriggerEnter2D(Collider2D other) //When entering a trigger.
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>(); //Find the DamageDealer of other.

        if(damageDealer != null) //If other has a DamageDealer (is a ship or projectile).
        {
            if(!immuneToDamage) //If the damage immunity power up is not active.
            {
                TakeDamage(damageDealer.GetDamage()); //Take the amount of damage the other DamageDealer deals.
                if(damageDealer.GetDamage() > 0) //If it is dealing damage (not a healing power up)
                {
                    PlayHitEffect(); //Play the hit particle effect.
                    audioPlayer.PlayDamageClip(); //Play the hit audio clip.
                    ShakeCamera(); //Shake the camera.
                    damageDealer.Hit(); //Destroy the other gameObject.
                }
                else //If it was a healing power up.
                {
                    player.PowerUpFinished(); //If it was a healing power up, //Indicate the power up has finished.
                }
            }
        }
    }

    void TakeDamage(int damage) //Take (or heal) the given amount.
    {
        health -= damage;
        if(health <= 0)
        {
                Die();
        }
        if(health > maxHealth) //Prevents healing over max health.
        {
            health = maxHealth;
        }
    }

    void Die()
    {
        if(!isPlayer) //If not the player, increase score by given amount.
        {
            scoreKeeper.AddScore(scoreIncrease);
        }
        else //Otherwise stop the timer and change to game over scene.
        {
            scoreKeeper.StopTimer();
            levelManager.LoadGameOver();
        }
        if(spawnsPowerUpOnDeath)
        {
            powerUpSpawner.SpawnPowerUp(powerups, powerUpSpeed, powerUpLifeTime, gameObject); //Spawns a random powerup from the powerups list.
        }
        Destroy(gameObject); //Destroy the game object.
    }

    void PlayHitEffect() //Play hit particle effect.
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera() //Shake the camera if a player.
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void ActivateImmunityPowerUp(float immunityLength)
    {
        StartCoroutine(PlayerIsImmune(immunityLength));
        StartCoroutine(PlayImmunityEffect());
    }

    IEnumerator PlayerIsImmune(float powerUpLength)
    {
        immuneToDamage = true;
        yield return new WaitForSecondsRealtime(powerUpLength);
        immuneToDamage = false;
        player.PowerUpFinished();
    }

    IEnumerator PlayImmunityEffect() //Make player flash visible and invisible. 
    {
        while(immuneToDamage)
        {
            spriteRenderer.sortingLayerName = "Default";
            yield return new WaitForSecondsRealtime(0.1f);
            spriteRenderer.sortingLayerName = "Ships";
            yield return new WaitForSecondsRealtime(0.1f);
        }
        spriteRenderer.sortingLayerName = "Ships";
    }

    
}
