using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SwitchLayerOnSocket : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnSocketEnter);
    }

    private void OnSocketEnter(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRSocketInteractor)
        {
            LayerMask bulletLayer = LayerMask.NameToLayer("Bullet");
            SwitchLayer(transform, bulletLayer);
        }
    }

    private void SwitchLayer(Transform objectToSwitch, LayerMask newLayer)
    {
        objectToSwitch.gameObject.layer = newLayer;
        foreach (Transform child in objectToSwitch)
        {
            child.gameObject.layer = newLayer;
            
            if (child.GetComponentInChildren<Transform>() != null)
            {
                SwitchLayer(child, newLayer);
            }
        }
    }
    
}
