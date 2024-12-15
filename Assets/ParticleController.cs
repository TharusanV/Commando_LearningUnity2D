using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private void FinishAnim(){
        Destroy(gameObject); //If there are multiple particles one after another, then instead use object pooling, where you don't delete the object, just take a object from a pool (array) then once your done with it put it back in the array 
    }
}
