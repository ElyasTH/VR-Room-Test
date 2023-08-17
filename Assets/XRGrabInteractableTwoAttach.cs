using System.Collections;
using System.Collections.Generic;
using MJUtilities;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public Transform leftAttach;
    public Transform rightAttach;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            AttachHandPoseData handData = args.interactorObject.transform.GetComponent<AttachHandPoseData>();

            if (handData.handType == AttachHandPoseData.HandModelType.Right)
                attachTransform = rightAttach;
            else
                attachTransform = leftAttach;
        }
        
        base.OnSelectEntering(args);
    }
}
