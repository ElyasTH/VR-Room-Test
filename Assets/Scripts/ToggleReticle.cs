using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleReticle : MonoBehaviour
{
    
    private XRInteractorReticleVisual reticle;
    void Start(){
        
        reticle = GetComponent<XRInteractorReticleVisual>();

    }

    public void Enable(){
        reticle.enabled = true;
    }

    public void Disable(){
        reticle.enabled = false;
    }

}
