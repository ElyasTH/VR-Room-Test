using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MJUtilities;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using XRGrabInteractable = UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable;

public class HandOutlineController : MonoBehaviour
{
   public enum HandType
   {
      Left,
      Right
   }

   public HandType handType;
   
   [SerializeField] private XRDirectInteractor xrDirectInteractor;
   [SerializeField] private XRInteractionManager xrInteractionManager;
   
   [SerializeField] private List<XRGrabInteractable> hoveredInteractables = new List<XRGrabInteractable>();

   private bool leftHandOutline;
   private bool rightHandOutline;

   private void Reset()
   {
      xrDirectInteractor = GetComponent<XRDirectInteractor>();
      xrInteractionManager = xrDirectInteractor.interactionManager;
   }

   private void OnEnable()
   {
      xrDirectInteractor.hoverEntered.AddListener(OnHoverEntered);
      xrDirectInteractor.hoverExited.AddListener(OnHoverExited);
      xrDirectInteractor.selectEntered.AddListener(OnSelectEntered);
      xrDirectInteractor.selectExited.AddListener(OnSelectExited);
   }

   private void OnDisable()
   {
      xrDirectInteractor.hoverEntered.RemoveListener(OnHoverEntered);
      xrDirectInteractor.hoverExited.RemoveListener(OnHoverExited);
      xrDirectInteractor.selectEntered.RemoveListener(OnSelectEntered);
      xrDirectInteractor.selectExited.RemoveListener(OnSelectExited);
   }

   private void Awake()
   {
      leftHandOutline = true;
      rightHandOutline = true;
   }
   
   private void Update()
   {
      if (CanOutline() && hoveredInteractables.Count > 1)
      {
         FindSelectedInteractable();
      }
   }

   private void FindSelectedInteractable()
   {
      foreach (var interactable in hoveredInteractables)
      {
         if (xrInteractionManager.IsHighestPriorityTarget(interactable))
         {
            SelectionManager.instance.Select(interactable.transform, handType);
            return;
         }
      }
   }
   
   private void OnHoverEntered(HoverEnterEventArgs args)
   {
      if (args.interactableObject is XRGrabInteractable)
      {
         hoveredInteractables.Add(args.interactableObject.transform.GetComponent<XRGrabInteractable>());
         
         if (CanOutline() && hoveredInteractables.Count == 1)
         {
            SelectionManager.instance.Select(hoveredInteractables[0].transform, handType);
         }
      }
   }
   
   private void OnHoverExited(HoverExitEventArgs args)
   {
      if (args.interactableObject is XRGrabInteractable)
      {
         hoveredInteractables.Remove(args.interactableObject.transform.GetComponent<XRGrabInteractable>());
         
         if (CanOutline() && hoveredInteractables.Count == 1)
         {
            SelectionManager.instance.Select(hoveredInteractables[0].transform, handType);
         }
         else if (CanOutline() && hoveredInteractables.Count == 0)
         {
            SelectionManager.instance.Deselect(handType);
         }
      }
   }
   

   private void OnSelectEntered(SelectEnterEventArgs args)
   {
      if (handType == HandType.Left) leftHandOutline = false;
      else rightHandOutline = false;
      SelectionManager.instance.Deselect(handType);
   }
   private void OnSelectExited(SelectExitEventArgs args)
   {
      if (handType == HandType.Left) leftHandOutline = true;
      else rightHandOutline = true;
      
      if (CanOutline() && hoveredInteractables.Count == 1)
      {
         SelectionManager.instance.Select(hoveredInteractables[0].transform, handType);
      }
      else if (CanOutline() && hoveredInteractables.Count == 0)
      {
         SelectionManager.instance.Deselect(handType);
      }
      else
      {
         FindSelectedInteractable();
      }
   }

   private bool CanOutline()
   {
      return (handType == HandType.Left && leftHandOutline) || (handType == HandType.Right && rightHandOutline);
   }

   // private void OnTriggerEnter(Collider other)
   // {
   //    var otherGrabInteractable = other.GetComponent<XRGrabInteractable>();
   //    if (otherGrabInteractable)
   //    {
   //       hoveredInteractables.Add(otherGrabInteractable);
   //       
   //       if (hoveredInteractables.Count == 1)
   //       {
   //          SelectionManager.instance.Select(hoveredInteractables[0].transform);
   //       }
   //    }
   // }

   // private void OnTriggerExit(Collider other)
   // {
   //    var otherGrabInteractable = other.GetComponent<XRGrabInteractable>();
   //    if (otherGrabInteractable)
   //    {
   //       hoveredInteractables.Remove(otherGrabInteractable);
   //       
   //       if (hoveredInteractables.Count == 1)
   //       {
   //          SelectionManager.instance.Select(hoveredInteractables[0].transform);
   //       }
   //       else if (hoveredInteractables.Count == 0)
   //       {
   //          SelectionManager.instance.Deselect();
   //       }
   //    }
   // }

}
