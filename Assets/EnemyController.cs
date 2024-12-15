using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State{
        Moving,Knockback,Dead
    }

    private State currentState;

    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        maxHealth,
        knockbackDuration;
        //lastTouchDamageTime,
        //touchDamageCooldown,touchDamage,touchDamageWidth,touchDamageHeight;

    [SerializeField]
    private Transform
        groundCheck,
        wallCheck;
        //touchDamageCheck;
    
    [SerializeField]
    private LayerMask 
        whatIsGround,
        whatIsPlayer;
    
    [SerializeField]
    private Vector2 knockbackSpeed;

    [SerializeField]
    private GameObject
        hitParticle;
        //deathChunkParticle,
        //deathBloodParticle;

    private float 
        currentHealth,
        knockbackStartTime;

    private float[] attackDetails = new float[2];

    private int damageDirection;

    public int facingDirection = -1;

    private Vector2 
        movement;
        //touchDamageBotLeft,
        //touchDamageTopRight;

    private bool
        groundDetected,
        wallDetected;

    private GameObject aliveObj;
    private Rigidbody2D aliveObjRb;
    private Animator aliveObjAnim;

    private void Start(){
        aliveObj = transform.Find("Alive").gameObject; //Pretty much we are finding the object that matches the name 'alive' out of all the child objects attached to the parent object of where this script is
        aliveObjRb = aliveObj.GetComponent<Rigidbody2D>();
        aliveObjAnim = aliveObj.GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    private void Update(){
        switch (currentState){
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    //--WALKING STATE--------------------------------------------------------------------------------

    private void EnterMovingState(){

    }

    private void UpdateMovingState(){
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        //CheckTouchDamage();

        if(!groundDetected || wallDetected){
            Flip();
        }
        else{
            movement.Set(movementSpeed * facingDirection, aliveObjRb.velocity.y); //We declared a Vector2D movement variable instead of creating a new one each time. This set funcion takes an x and y
            aliveObjRb.velocity = movement;
        }
    }

    private void ExitMovingState()
    {

    }

    //--KNOCKBACK STATE-------------------------------------------------------------------------------

    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time; //Keeps track of the exact time when the knockback starts
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveObjRb.velocity = movement;
        aliveObjAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        aliveObjAnim.SetBool("Knockback", false);
    }

    //--DEAD STATE---------------------------------------------------------------------------------------

    private void EnterDeadState()
    {
        //Instantiate(deathChunkParticle, aliveObj.transform.position, deathChunkParticle.transform.rotation);
        //Instantiate(deathBloodParticle, aliveObj.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //--OTHER FUNCTIONS--------------------------------------------------------------------------------

    private void Damage(float[] attackDetails) //We are using an array as the sendMessage function (which we are using when the player hits something) as it only allows for one paramater to be sent in so we need to use an array to send multiple parameters, in this case the attackDamage, X location of the person doing the attack to determine which side the person is standing on to know where they should be knocked back
    {
        currentHealth -= attackDetails[0]; //Attack damage will be at 0 index

        Instantiate(hitParticle, aliveObj.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(attackDetails[1] > aliveObj.transform.position.x){ //the attacker's x position would be the 1 index
            damageDirection = -1; //This means the attacker/player is facing the enemy
        }
        else{
            damageDirection = 1; //This means the attack/player is not facing the enemy
        }

        //Hit particle
        if(currentHealth > 0.0f){ //Enemy still alive
            SwitchState(State.Knockback);
        }
        else if(currentHealth <= 0.0f){
            SwitchState(State.Dead);
        }
    }

    /*
    private void CheckTouchDamage()
    {
        if(Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if(hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = aliveObj.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }
    */

    private void Flip()
    {
        facingDirection *= -1;
        aliveObj.transform.Rotate(0.0f, 180.0f, 0.0f);

    }

    private void SwitchState(State state){
        switch (currentState){
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state){
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        /*
        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
        */
    }

}
