using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;

public class PlayerBodyScript : MonoBehaviour
{

    public Sprite bodyLookFullyUp;
    public Sprite bodyLookUp;
    public Sprite bodyLookMiddle;
    public Sprite bodyLookDown;
    public Sprite bodyLookFullyDown;

    private SpriteRenderer sr;

    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        UpdateSprite();
    }

    void UpdateSprite(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 direction = mousePos - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Debug.Log(angle+5);

        //Left
        if(direction.x < 0){
            if(105 > angle + 5 && angle + 5 > 90){
                if(sr.sprite != bodyLookFullyUp){
                    sr.sprite = bodyLookFullyUp;
                }
            }
            else if(120 > angle + 5 && angle + 5 > 105){
                if(sr.sprite != bodyLookUp){
                    sr.sprite = bodyLookUp;
                }
            }
            else if(150 > angle + 5 && angle + 5 > 120){
                if(sr.sprite != bodyLookMiddle){
                    sr.sprite = bodyLookMiddle;
                }
            }
            else if(165 > angle + 5 && angle + 5 > 150){
                if(sr.sprite != bodyLookDown){
                    sr.sprite = bodyLookDown;
                }
            }
            else{
                if(sr.sprite != bodyLookFullyDown){
                    sr.sprite = bodyLookFullyDown;
                }
            }
        }

        //Right
        else if(direction.x > 0){
            if(90 > angle + 5 && angle + 5 > 75){
                if(sr.sprite != bodyLookFullyUp){
                    sr.sprite = bodyLookFullyUp;
                }
            }
            else if(75 > angle + 5 && angle + 5 > 60){
                if(sr.sprite != bodyLookUp){
                    sr.sprite = bodyLookUp;
                }
            }
            else if(60 > angle + 5 && angle + 5 > 30){
                if(sr.sprite != bodyLookMiddle){
                    sr.sprite = bodyLookMiddle;
                }
            }
            else if(30 > angle + 5 && angle + 5 > 15){
                if(sr.sprite != bodyLookDown){
                    sr.sprite = bodyLookDown;
                }
            }
            else{
                if(sr.sprite != bodyLookFullyDown){
                    sr.sprite = bodyLookFullyDown;
                }
            }
        }


        
    }

}
