using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This component handles hover animations for a UI button.
/// It uses the UIAnimations helper class to perform the animations.
/// Attach this script to any UI button that needs hover effects.
/// </summary>
public class ButtonExpandAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("The RectTransform of the button to animate. If left null, it will be automatically fetched.")]
    public RectTransform rTransform;

    [Tooltip("The duration of the hover animations.")]
    public float duration = 0.1f;

    [Tooltip("The amount to scale the button on hover.")]
    public float scale = 1.1f;

    [Tooltip("The color to change the text to on hover.")]
    public Color targetColor = Color.white;

    // Private variables to store the initial state
    private TextMeshProUGUI _text;
    private Color _originalColor;

    private void Start()
    {
        // If the RectTransform is not assigned in the inspector, get it from the component.
        if (rTransform == null)
        {
            rTransform = GetComponent<RectTransform>();
        }
        
        // Get the TextMeshProUGUI component from the children.
        _text = GetComponentInChildren<TextMeshProUGUI>();
        if (_text != null)
        {
            // Store the original color of the text to revert to on hover exit.
            _originalColor = _text.color;
        }
    }

    /// <summary>
    /// Called when the mouse pointer enters the UI element.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Trigger the hover enter animations.
        Benetti.UIAnimations.onHoverEnter(rTransform, scale, duration);
        if (_text != null)
        {
            Benetti.UIAnimations.onHoverColor(_text, targetColor, duration);
        }
    }

    /// <summary>
    /// Called when the mouse pointer exits the UI element.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        // Trigger the hover exit animations.
        Benetti.UIAnimations.onHoverExit(rTransform, duration);
        if (_text != null)
        {
            Benetti.UIAnimations.onHoverExitColor(_text, _originalColor, duration);
        }
    }
}
