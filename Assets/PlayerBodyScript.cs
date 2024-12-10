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

        if(angle + 5 < 90 && angle + 5 > 65){
            if(sr.sprite != bodyLookFullyUp){
                sr.sprite = bodyLookFullyUp;
            }
        }
    }

}
