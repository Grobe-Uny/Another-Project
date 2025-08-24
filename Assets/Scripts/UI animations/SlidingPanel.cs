using UnityEngine;

/// <summary>
/// Controls a UI panel that can slide on and off the screen.
/// Uses anchoredPosition for reliable positioning on different screen sizes.
/// </summary>
public class SlidingPanel : MonoBehaviour
{
    [Tooltip("The RectTransform of the object to slide. Will be fetched from this GameObject if null.")]
    public RectTransform rTransform;

    [Tooltip("The Y position of the panel (using Anchored Position) when it's visible on the screen.")]
    public float onScreenYPosition = 0f;

    [Tooltip("The Y position of the panel (using Anchored Position) when it's hidden off-screen.")]
    public float offScreenYPosition = -1500f;

    [Tooltip("How long the slide animation should take.")]
    public float animationTime = 0.5f;

    private bool _isAnimating = false;

    void Awake()
    {
        if (rTransform == null)
        {
            rTransform = GetComponent<RectTransform>();
        }
    }

    /// <summary>
    /// Slides the panel into view (to the on-screen position).
    /// </summary>
    public void Show()
    {
        if (_isAnimating) return;
        _isAnimating = true;

        // Make sure the object is active and in the off-screen position before starting
        gameObject.SetActive(true);
        rTransform.anchoredPosition = new Vector2(rTransform.anchoredPosition.x, offScreenYPosition);

        // Animate the panel to its on-screen position
        LeanTween.moveY(rTransform, onScreenYPosition, animationTime)
            .setEaseOutExpo()
            .setOnComplete(() => _isAnimating = false);
    }

    /// <summary>
    /// Slides the panel out of view (to the off-screen position) and then disables it.
    /// </summary>
    public void Hide()
    {
        if (_isAnimating) return;
        _isAnimating = true;

        // Animate the panel to its off-screen position and disable it upon completion
        LeanTween.moveY(rTransform, offScreenYPosition, animationTime)
            .setEaseInExpo()
            .setOnComplete(() =>
            {
                gameObject.SetActive(false);
                _isAnimating = false;
            });
    }
}
