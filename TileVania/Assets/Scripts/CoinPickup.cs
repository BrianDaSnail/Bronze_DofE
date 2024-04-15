using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSFX;
    [SerializeField] private int coinValue = 100;
    bool coinPickedUp;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !coinPickedUp) //When coin touched, play pickup sound, add score, and destroy coin.
        {
                coinPickedUp = true;
                AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
                Debug.Log("Coin Picked Up");
                FindObjectOfType<GameSession>().CoinPickup(coinValue);
                Destroy(gameObject);
        }   
    }
}
