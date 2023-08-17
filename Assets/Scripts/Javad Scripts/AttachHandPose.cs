using MJUtilities;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using XRGrabInteractable = UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable;

public class AttachHandPose : MonoBehaviour
{
    private XRGrabInteractable xrGrabInteractable;

    [SerializeField] private Transform rightHandPose;
    [SerializeField] private Transform leftHandPose;

    public Transform RightHandPose => rightHandPose;
    public Transform LeftHandPose => leftHandPose;
    public Transform currentHandPose;


    private void Start()
    {
        xrGrabInteractable = GetComponent<XRGrabInteractable>();
        currentHandPose = xrGrabInteractable.attachTransform;
    }

    public void SetupPose(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            // if(args.interactorObject.transform.GetComponent<XRDirectInteractor>().hasSelection) return;
            // if(xrGrabInteractable.isSelected) return;
            
            AttachHandPoseData handData = args.interactorObject.transform.GetComponent<AttachHandPoseData>();

            if (handData.handType == AttachHandPoseData.HandModelType.Right)
                SetAttachTransform(rightHandPose);
            else
                SetAttachTransform(leftHandPose);
        }
    }
    
    public void SetAttachTransform(Transform targetTransform)
    {
        xrGrabInteractable.attachTransform = targetTransform;
        currentHandPose = xrGrabInteractable.attachTransform;
    }

    #region TOOL

    [ContextMenu(nameof(TOOL_SetAttachTransformForXRGrabInteractable))]
    private void TOOL_SetAttachTransformForXRGrabInteractable()
    {
        if(!xrGrabInteractable)
            xrGrabInteractable = GetComponent<XRGrabInteractable>();

        xrGrabInteractable.attachTransform = rightHandPose;
    }
    

    #endregion
}