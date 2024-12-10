using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 16f;
    private float movementInputDirection;


    private Rigidbody2D rb;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        CheckInput();
    }

    private void FixedUpdate(){
        ApplyMovement();
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
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

}
