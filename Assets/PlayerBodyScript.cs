using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
