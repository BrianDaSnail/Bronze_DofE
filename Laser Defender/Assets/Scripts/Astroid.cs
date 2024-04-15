using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Astroid : MonoBehaviour
{

    [SerializeField] float maxProjectileSpeed = 20f;
    [SerializeField] float minProjectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] UnityEngine.Vector3 teleportPosition;

    Rigidbody2D myRigidBody;


    void Awake()
    {
        myRigidBody = FindObjectOfType<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TeleportAstroid();
        AstroidMovement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TeleportAstroid() //Teleports the astroid to a random position along the x-axis, above the game.
    {
        float teleportxPosition = Random.Range(-5,5);
        teleportPosition = new UnityEngine.Vector3(teleportxPosition, 10, Mathf.Epsilon);
        transform.position = teleportPosition;

    }

    void AstroidMovement() //Moves the astroid downards untill it reaches the end of its life time and destroyed.
    {
        if(myRigidBody != null)
        {
            myRigidBody.velocity = transform.up * Random.Range(minProjectileSpeed,maxProjectileSpeed);

        }
        Destroy(gameObject, projectileLifeTime);
    }






}
