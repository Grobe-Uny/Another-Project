using System;
using UnityEngine;

public class ButtonExpandAnim : MonoBehaviour
{
    public RectTransform rTransform;
    public float duration;
    public float scale;
    public Color targetColor;
    public float originalScale;
    private void Start()
    {
        
    }

    public void OnMouseEnter()
    {
        Benetti.UIAnimations.onHoverEnter();
        Benetti.UIAnimations.onHoverColor();
    }
}
