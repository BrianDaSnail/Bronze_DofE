using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpConfigSO powerUpConfig;
    [SerializeField] bool isHealthPowerUp;

    public PowerUpConfigSO GetPowerUpConfig()
    {
        return powerUpConfig;
    }

    private void OnTriggerEnter2D(Collider2D other)  //If a health power up collides with the player, destroy itself.
    {
        if(isHealthPowerUp && other.tag == "Player")
        {
            Destroy(gameObject);
        }    
    }

    public bool IsHealthPowerUp()
    {
        return isHealthPowerUp;
    }
    



}
