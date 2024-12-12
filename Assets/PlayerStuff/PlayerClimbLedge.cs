using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbLedge : MonoBehaviour
{

    //NOT USED - SO THE CODE IS NOT FINISHED

    //Ledge Jump
    private bool isTouchingLedge = false;
    public Transform ledgeCheckObj;
    private bool canClimbLedge = false;
    private bool ledgeDetected;
    private Vector2 ledgePositionBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;
    public float ledgeClimbXOffset1 = 0f; 
    public float ledgeClimbYOffset1 = 0f; 
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;


    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         CheckLedgeClimb();       
    }

    private void CheckSurroundings(){
        
        isTouchingLedge = Physics2D.Raycast(ledgeCheckObj.position, transform.right, wallCheckDistance, whatIsGround);
        
        if(isTouchingWall && !isTouchingLedge && !ledgeDetected){
            ledgeDetected = true;
            ledgePositionBot = wallCheckObj.position;
        }
    }

    private void CheckLedgeClimb(){
        if(ledgeDetected && !canClimbLedge){
            canClimbLedge = true;
        }
    }
    */
}
