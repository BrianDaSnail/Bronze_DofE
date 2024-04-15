using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{


    public void SpawnPowerUp(List<GameObject> powerUps, float powerUpSpeed, float powerUpLifeTime, GameObject spawner)  //Spawns a random power up from a power up list at the spawner, travelling downards.
    {
        GameObject instance = Instantiate(powerUps[Random.Range(0, powerUps.Count)], spawner.transform.position, Quaternion.identity);
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * -powerUpSpeed;
            }

            Destroy(instance, powerUpLifeTime); //Destroy the powerup once its life time has run out.

    }
}
