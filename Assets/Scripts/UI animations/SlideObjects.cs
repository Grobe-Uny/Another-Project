using UnityEngine;
using Benetti;

public class SlideObjects : MonoBehaviour
{
    public static RectTransform rTransform;
    public static float newPosition;
    public static float originalPosition;
    public static float animationTime;
    
    void Start()
    {
        if (rTransform == null)
        {
            rTransform = GetComponent<RectTransform>();
        }

        originalPosition = rTransform.position.y;
    }

    public static void AnimateIn()
    {
        UIAnimations.SlideYUILinear(rTransform, newPosition, animationTime);
    }
    public static void AnimateOut()
    {
        UIAnimations.SlideYUILinearWithDisable(rTransform, originalPosition, animationTime);
    }
}
