using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 16f;

    public int amountOfJumps = 1;
    private int amountOfJumpsLeft;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private bool canMove;
    private bool canJump;

    private float movementInputDirection;


    public Transform groundCheckObj;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isMove = false;
    private bool isKnifeAttack = false;
    private bool isGrounded = false;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start(){
        amountOfJumpsLeft = amountOfJumps;
    }

    // Update is called once per frame
    void Update(){
        CheckInput();
        CheckMovementDirection();
        UpdateAnimator();
        CheckIfCanJump();
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
        rb.velocity = new Vector2(movementInputDirection * movementSpeed, rb.velocity.y);
    }

    private void Jump(){
        if(canJump){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
    }

    private void UpdateAnimator(){
        animator.SetBool("isMove", isMove);
    }

    private void CheckSurroundings(){
        isGrounded = Physics2D.OverlapCircle(groundCheckObj.position, groundCheckRadius, whatIsGround);
    }

    
    private void OnDrawGizmos (){
        Gizmos.DrawWireSphere(groundCheckObj.position, groundCheckRadius);
    }

    private void CheckIfCanJump(){
        if(isGrounded && rb.velocity.y <= 0){
            amountOfJumpsLeft = amountOfJumps;
        }

        if(amountOfJumpsLeft <= 0){
            canJump = false;
        }
        else{
            canJump = true;
        }
    }
}
