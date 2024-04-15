using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 300f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float slowSpeed = 7.5f;
    [SerializeField] float boostSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.collider.tag == "SpeedBump")
        {
            Debug.Log("Hit speed bump.");
            moveSpeed = slowSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Hit trigger.");
            
        if (other.tag == "SpeedBoost")
        {
            Debug.Log("Hit speed boost.");
            moveSpeed = boostSpeed;
        }
    }    
}
