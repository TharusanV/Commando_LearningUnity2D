using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponArmScript : MonoBehaviour
{

    public Sprite knifeSpriteIdle;
    public Sprite swordSpriteIdle;
    


    private SpriteRenderer sr;

    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
