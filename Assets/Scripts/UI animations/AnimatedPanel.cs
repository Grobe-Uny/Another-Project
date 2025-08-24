using UnityEngine;

// Attach this script to any UI Panel you want to animate.
public class AnimatedPanel : MonoBehaviour
{
    public float animationDuration = 0.3f;
    private Vector3 _originalScale;
    private bool _isAnimating = false;

    void Awake()
    {
        // Store the original scale to animate back to.
        _originalScale = transform.localScale;
    }

    public void Show()
    {
        // Prevent issues if called while already animating
        if (_isAnimating) return;
        _isAnimating = true;

        // Start from a zero scale and activate the object
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);

        // Animate the panel to its original size
        LeanTween.scale(gameObject, _originalScale, animationDuration)
            .setEaseOutBack()
            .setOnComplete(() => _isAnimating = false); // Reset flag when done
    }

    public void Hide()
    {
        if (_isAnimating) return;
        _isAnimating = true;

        // Animate the panel down to zero scale, then disable it
        LeanTween.scale(gameObject, Vector3.zero, animationDuration)
            .setEaseInBack()
            .setOnComplete(() =>
            {
                gameObject.SetActive(false);
                _isAnimating = false; // Reset flag when done
            });
    }
}
