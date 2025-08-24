using UnityEngine;
using Benetti;

public class SlideObjects : MonoBehaviour
{
    public RectTransform rTransform;
    public float newPosition;
    public float originalPosition;
    public float animationTime;
    
    void Start()
    {
        if (rTransform == null)
        {
            rTransform = GetComponent<RectTransform>();
        }

        originalPosition = rTransform.position.y;
    }

    public void OnEnable()
    {
        UIAnimations.SlideYUILinear(rTransform, newPosition, animationTime);
    }
}
