using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RTLTMPro;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public class ButtonColor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    private Image borderImage;
    private RTLTextMeshPro text;
    
    [Header("Button Color")]
    public Color normalColor;
    public Color hoverColor;
    public Color clickColor;
    
    [Header("Text Color")]
    public Color textNormalColor;
    public Color textHoverColor;
    public Color textClickColor;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        foreach (var image in GetComponentsInChildren<Image>())
        {
            if (image != buttonImage)
                borderImage = image;
        }

        text = GetComponentInChildren<RTLTextMeshPro>();

        buttonImage.color = normalColor;
        try{
            borderImage.color = textNormalColor;
            text.color = textNormalColor;
        } catch(NullReferenceException)
        {
            Debug.Log("No border or text found");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = hoverColor;
        try
        {
            borderImage.color = textHoverColor;
            text.color = textHoverColor;
        } catch(NullReferenceException)
        {
            Debug.Log("No border or text found");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = normalColor;
        try{
            borderImage.color = textNormalColor;
            text.color = textNormalColor;
        } catch(NullReferenceException)
        {
            Debug.Log("No border or text found");
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.color = clickColor;
        try{
            borderImage.color = textClickColor;
            text.color = textClickColor;
        } catch(NullReferenceException)
        {
            Debug.Log("No border or text found");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.color = hoverColor;
        try{
            borderImage.color = textHoverColor;
            text.color = textHoverColor;
        } catch(NullReferenceException)
        {
            Debug.Log("No border or text found");
        }
    }
}