﻿using System.Collections.Generic;
 using MJUtilities;
 using UnityEditor;
 using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;
    
    private ISelectionResponse _selectionResponse;

    private Transform _LeftHandSelection;
    private Transform _RightHandSelection;

    [SerializeField] private List<Outline> outlines = new List<Outline>();

    private void Awake()
    {
        if (!instance)
            instance = this;
        
        _selectionResponse = GetComponent<ISelectionResponse>();
    }


    public void Select(Transform targetTransform, HandOutlineController.HandType handType)
    {
        if (handType == HandOutlineController.HandType.Left)
        {
            if (_LeftHandSelection) _selectionResponse.OnDeselect(_LeftHandSelection);

            _LeftHandSelection = targetTransform;

            if (_LeftHandSelection) _selectionResponse.OnSelect(_LeftHandSelection);
        }
        else
        {
            if (_RightHandSelection) _selectionResponse.OnDeselect(_RightHandSelection);

            _RightHandSelection = targetTransform;

            if (_RightHandSelection) _selectionResponse.OnSelect(_RightHandSelection);
        }
        
    }
     public void Deselect(HandOutlineController.HandType handType)
    {
        if (handType == HandOutlineController.HandType.Left)
        {
            if (_LeftHandSelection)
                _selectionResponse.OnDeselect(_LeftHandSelection);

            _LeftHandSelection = null;
        }
        else
        {
            if (_RightHandSelection)
                _selectionResponse.OnDeselect(_RightHandSelection);

            _RightHandSelection = null;
        }
    }

     public void SetNewSelection(Transform targetTransform, Transform interactorTransform)
     {
         // if (_selection)
         // {
         //     if(_selection == targetTransform) return;
         //     
         //     if (Vector3.Distance(interactorTransform.position, targetTransform.position) < Vector3.Distance(interactorTransform.position, _selection.position))
         //     {
         //         _selection = targetTransform;
         //         _selectionResponse.OnSelect(_selection);
         //     }
         // }
         // else
         // {
         //     _selection = targetTransform;
         //     _selectionResponse.OnSelect(_selection);
         // }
     }
     
    
    // private void Update()
    // {
    //     if (_selection != null) _selectionResponse.OnDeselect(_selection);
    //
    //     var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     
    //     _selection = null;
    //     if (Physics.Raycast(ray, out var hit))
    //     {
    //         var selection = hit.transform;
    //         if (selection.CompareTag(selectableTag))
    //         {
    //             _selection = selection;
    //         }
    //     }
    //
    //     if (_selection != null) _selectionResponse.OnSelect(_selection);
    // }
    
    private void TOOL_SetReferences()
    {
        outlines.Clear();

        var tmpArray = FindObjectsOfType<Outline>();

        foreach (var outline in tmpArray)
        {
            outlines.Add(outline);
        }
    }
    
    
#if UNITY_EDITOR
    
    [CustomEditor(typeof(SelectionManager))]
    public class RegulatorPartCreatorCustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            

            SelectionManager selectionManager = (SelectionManager) target;
            
            EditorGUILayout.Space(20f);
            
            if (GUILayout.Button("Fill Outline List"))
            {
                selectionManager.TOOL_SetReferences();
            }
        }
    }
    
#endif
}