using UnityEngine;
using UnityEngine.UI;

public class CustomizeChildButtons : MonoBehaviour
{
    
    [Header("Button Colors")]
    public Color buttonNormalColor;
    public Color buttonHoverColor;
    public Color buttonClickColor;
    
    [Header("Text Colors")]
    public Color fontNormalColor;
    public Color fontHoverColor;
    public Color fontClickColor;
    
    [ContextMenu("Apply Settings")]
    public void ApplySettings()
    {
        foreach (var button in GetComponentsInChildren<Button>())
        {
            var buttonColor = button.gameObject.GetComponent<ButtonColor>();
            if (!buttonColor)
                buttonColor = button.gameObject.AddComponent<ButtonColor>();
            
            buttonColor.normalColor = buttonNormalColor;
            buttonColor.hoverColor = buttonHoverColor;
            buttonColor.clickColor = buttonClickColor;

            buttonColor.textNormalColor = fontNormalColor;
            buttonColor.textHoverColor = fontHoverColor;
            buttonColor.textClickColor = fontClickColor;
        }
    }
}
