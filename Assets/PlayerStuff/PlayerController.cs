using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Move variables
    public float movementSpeed = 5f;
    private float movementInputDirection;
    public int facingDirection = -1;



     //Jumping
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public float wallCheckDistance;
    public float jumpForce = 16f;
    public int amountOfJumps = 1;
    private int amountOfJumpsLeft;
    public float movementForceInAir = 10;
    public float airDragMultiplier = 0.95f;
    public Transform groundCheckObj;

   
    //Wall Sliding/Jumping variables
    public Vector2 wallJumpDirection;
    public float wallJumpForce;
    public float wallSlidingSpeed;
    public float wallHopForce;
    public Vector2 wallHopDirection;

    public Transform wallCheckObj;

    //Other variables
    private Rigidbody2D rb;
    private Animator animator;

    //IS/CAN variables
    private bool isMove = false;
    private bool isKnifeAttack = false;
    private bool isGrounded = false;
    private bool isTouchingWall = false;
    private bool isWallSliding = false;
    private bool canMove;
    private bool canJump;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start(){
        amountOfJumpsLeft = amountOfJumps;
        wallJumpDirection.Normalize(); //This will make the vector itself equal to 1. 
        wallHopDirection.Normalize();
    }

    // Update is called once per frame
    void Update(){
        CheckInput();
        CheckMovementDirection();
        UpdateAnimator();
        CheckIfCanJump();
        CheckIfWallSliding();
    }

    private void FixedUpdate(){
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckMovementDirection(){
        if(rb.velocity.x != 0){
            isMove = true;
        }
        else{
            isMove = false;
        }
    }

    private void CheckInput(){
        movementInputDirection = Input.GetAxisRaw("Horizontal"); //A will return -1 and D will return 1

        if(Input.GetButtonDown("Jump")){
            Jump();
        }
    }

    private void ApplyMovement(){
        if(isGrounded){ 
            rb.velocity = new Vector2(movementInputDirection * movementSpeed, rb.velocity.y);
        }
        else if(!isGrounded && !isWallSliding && movementInputDirection != 0){
            Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0); //We will add the force to the x-direction so when jumping you can move as well
            rb.AddForce(forceToAdd);

            if(Mathf.Abs(rb.velocity.x) > movementSpeed){
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }
        }
        else if(!isGrounded && !isWallSliding && movementInputDirection == 0){
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }



        if (isWallSliding){
            if(rb.velocity.y < -wallSlidingSpeed){
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }
        }
    }

    private void Jump(){
        if(canJump && !isWallSliding){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
        //Wall hop
        else if(isWallSliding && movementInputDirection == 0 && canJump){
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
        //Wall jump
        else if((isWallSliding || isTouchingWall) && movementInputDirection != 0 && canJump){
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
    }

    private void UpdateAnimator(){
        animator.SetBool("isMove", isMove);
    }

    private void CheckSurroundings(){
        isGrounded = Physics2D.OverlapCircle(groundCheckObj.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheckObj.position, transform.right, wallCheckDistance, whatIsGround);
    }

    
    private void OnDrawGizmos (){
        Gizmos.DrawWireSphere(groundCheckObj.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheckObj.position, new Vector3(wallCheckObj.position.x + wallCheckDistance, wallCheckObj.position.y, wallCheckObj.position.z)); //Draw line this time as its a raycast
    }

    private void CheckIfCanJump(){
        if((isGrounded && rb.velocity.y <= 0) || isWallSliding){
            amountOfJumpsLeft = amountOfJumps;
        }

        if(amountOfJumpsLeft <= 0){
            canJump = false;
        }
        else{
            canJump = true;
        }
    }

    private void CheckIfWallSliding(){
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0){
            isWallSliding = true;
        }
        else{
            isWallSliding = false;
        }
    }
}
