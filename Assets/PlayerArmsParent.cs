using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerArmsParent : MonoBehaviour
{

    /*So if you are designing the game for mobile / console I would just make the weapon point towards an "invisible cursor" that we would offset from the player positions using the direction in which your right stick is pointing in. So when you move your right stick what you get as an input is the direction vector (representing the direction in which your are pushing the stick). You could send it directly to your weapon as the direction in which our weapon should be rotated. I hope it makes sense.*/
    public float offset = 5.0f;

    public GameObject playerObject;

    public float currentScalePlayerObject;

    void Awake(){
        currentScalePlayerObject = playerObject.transform.localScale.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 direction = mousePos - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        if(angle + offset > 0 && angle + offset < 180){
            transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);
            Debug.Log(angle+offset);

            Vector2 playerObjScale = playerObject.transform.localScale;
            Vector2 handsObjScale = transform.localScale;
            
            //Facing left
            if(direction.x < 0){
                playerObjScale.x = 1 * currentScalePlayerObject;
                handsObjScale.x = -1; 
                handsObjScale.y = -1;          
            }
            //Facing right
            else if(direction.x > 0){
                playerObjScale.x = -1 * currentScalePlayerObject;
                handsObjScale.x = 1;
                handsObjScale.y = 1;  
            }
            playerObject.transform.localScale = playerObjScale;
            transform.localScale = handsObjScale;
        }


    }
}
