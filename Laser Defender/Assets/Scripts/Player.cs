using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;
    PowerUp powerUp;
    PowerUpConfigSO currentPowerUp;

    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;
    Health health;


    void Awake()
    {
        shooter = gameObject.GetComponent<Shooter>();
        health = gameObject.GetComponent<Health>();
    }


        
    void Start()
    {
        InitBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void InitBounds() //Set the bounds for the permimeter of the game.
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    void Move() //Moves the player and prevents them from leaving a given area.
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }
    
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
       if(other.gameObject.tag == "Power Up" && currentPowerUp == null) //If the player collides with a powerup and does not already have a powerup.
       {
            powerUp = other.gameObject.GetComponent<PowerUp>();
            currentPowerUp = powerUp.GetPowerUpConfig();

            if(currentPowerUp.IsImmunityPowerUp()) //If it is an immunity power up, make the player immune to damage for a given number of seconds.
            {
                health.ActivateImmunityPowerUp(currentPowerUp.GetPowerUpLength());
            }
            if(currentPowerUp.IsProjectilePowerUp()) //If it is a projectile power up, give the player the new projectile, projectile speed, and firing rate for a given number of seconds.
            {
                StartCoroutine(shooter.ActivateProjectilePowerUp(currentPowerUp.GetProjectilePrefab(), currentPowerUp.GetProjectileSpeed(), currentPowerUp.GetFiringRate(), currentPowerUp.GetPowerUpLength()));
            }
            if(!powerUp.IsHealthPowerUp()) //Destroy the power up unless it is a health power up (destroyed by the power up itself).
            {
                Destroy(other.gameObject);
            }

       }
    }

    public void PowerUpFinished()
    {
        currentPowerUp = null;
    }


}
