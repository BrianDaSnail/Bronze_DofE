using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody2D;
    [SerializeField] private float health = 3f;    




    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody2D.velocity = new Vector2 (moveSpeed, 0f); //Enemy constantly moving. 

    }

    void  OnTriggerExit2D(Collider2D other) //Enemy flips when it reaches an edge.
    {
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody2D.velocity.x)), 1f);
    }
    

    void OnTriggerEnter2D(Collider2D other) //Enemy loses health when hit by bullet. Dies when health is 0.
    {
        if(other.tag == "Bullet")
        {
            health--;
            if(health == 0f)
            {
                Destroy(gameObject);
            }
        }       
    }

}
