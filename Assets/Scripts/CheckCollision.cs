using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    
    void OnParticleCollision(GameObject other){
        
        if (other.tag.Equals("Fire")){
            other.transform.parent.parent.GetComponent<FireToggle>().PutOut();
        }

    }

}
