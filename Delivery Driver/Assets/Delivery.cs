using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    bool hasPackage;

    [SerializeField] float destroyDelay = 0.5f;

    [SerializeField] Color32 hasPackageColour = new Color32 (1,1,1,1);
    [SerializeField] Color32 noPackageColour = new Color32 (1,1,1,1);
    [SerializeField] int packagesDelivered = 0;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Crash!");
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package picked up.");
            hasPackage = true;
            spriteRenderer.color = hasPackageColour;
            Destroy(other.gameObject, destroyDelay);
        }

        if (other.tag == "Customer" && hasPackage)
        {
            Debug.Log("Package delivered.");
            spriteRenderer.color = noPackageColour;
            hasPackage = false;
            packagesDelivered++;
            Destroy(other.gameObject, destroyDelay);
        }
        
    }
}
