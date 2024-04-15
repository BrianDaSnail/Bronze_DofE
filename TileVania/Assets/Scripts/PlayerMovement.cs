using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float gravityScale = 2f;
    [SerializeField] Vector2 deathKick = new Vector2 (0f, 10f);
    [SerializeField] PhysicsMaterial2D frictionMaterial;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    bool isAlive = true;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();
        if(isAlive)
        {
            Run();
            FlipSprite();
            ClimbLadder();
        }
    }

    void OnFire(InputValue value) //Allows player to shoot.
    {
        if(!isAlive) {return;}
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnMove(InputValue value) //Gets player input for the x and y axis.
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) //Allows player to jump when touching the ground.
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || !isAlive) {return;}

        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void Run() //Allows player to run.
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; 
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed); //Plays running animation when running and idling when not. 
    }

    void FlipSprite() //Flips the player to point in the direction they are running.
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; 

        if (playerHasHorizontalSpeed) 
        {
        transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }


    void ClimbLadder() //Allows player to climb.
    {
        if(!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) //When not touching ladder, disable climbing animation and reset gravity.
        {
            myRigidbody.gravityScale = gravityScale;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticleSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;  //Plays climbing animation when on ladder and moving on it.
        myAnimator.SetBool("isClimbing", playerHasVerticleSpeed);
    }


    void Die() //If touching an enemy, water, or a hazard. Kill player and play death animation.
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Water", "Hazards")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Water", "Hazards")))
        {
            if(isAlive)
            {
                myRigidbody.velocity = deathKick;
                myRigidbody.sharedMaterial = frictionMaterial;
            }
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
  