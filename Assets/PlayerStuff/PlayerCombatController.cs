using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{


    [SerializeField]
    private bool combatEnabled; 
    [SerializeField]
    private float inputTimer, knifeAttackRadius, knifeAttackDamage; 
    [SerializeField]
    private Transform knifeAttackHitBoxPosition;
    [SerializeField]
    private LayerMask whatIsDamageable;


    private bool gotInput, isAttacking, isKnifeAttack;

    private float lastInputTime = Mathf.NegativeInfinity; //Setting it to negative infinity will allow the player to attack as sonn as the game starts

    private float[] attackDetails = new float[2];

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("canAttack", combatEnabled);
    }

    // Update is called once per frame
    void Update(){
     CheckCombatInput();
     CheckAttacks();   
    }

    private void CheckCombatInput(){
        if(Input.GetMouseButtonDown(0)){
            if (combatEnabled){
                //Attempt combat - We are gonna hold combat so that if the player tries to hit combat a bit before they are able to, then they will hit once they are able to
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks(){
        if(gotInput){
            if(!isAttacking){
                gotInput = false;
                isAttacking = true;
                animator.SetBool("isKnifeAttack", true);
                animator.SetBool("isAttacking", isAttacking);

            }
        }
        if(Time.time >= lastInputTime + inputTimer){
            gotInput = false;
        }
    }

    private void CheckKnifeAttackHitBox(){
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(knifeAttackHitBoxPosition.position, knifeAttackRadius, whatIsDamageable);

        attackDetails[0] = knifeAttackDamage;
        attackDetails[1] = transform.position.x;

        foreach(Collider2D collider in detectedObjects){
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private void FinishKnifeAttack(){
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isKnifeAttack", false);
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(knifeAttackHitBoxPosition.position, knifeAttackRadius);
    }

}
