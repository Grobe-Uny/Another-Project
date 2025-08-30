using System;
using TMPro;
using UnityEngine;

namespace Benetti
{
    /// <summary>
    /// A helper class for handling common UI animations using LeanTween.
    /// </summary>
    public class UIAnimations 
    {
        /// <summary>
        /// Scales a UI element up on hover.
        /// </summary>
        /// <param name="target">The RectTransform of the UI element to scale.</param>
        /// <param name="scale">The target scale multiplier.</param>
        /// <param name="duration">The duration of the animation.</param>
        public static void onHoverEnter(RectTransform target, float scale, float duration)
        {
            LeanTween.scale(target, Vector3.one * scale, duration).setEaseOutQuad();
        }

        /// <summary>
        /// Scales a UI element back to its original size on hover exit.
        /// </summary>
        /// <param name="target">The RectTransform of the UI element to scale.</param>
        /// <param name="duration">The duration of the animation.</param>
        public static void onHoverExit(RectTransform target, float duration)
        {
            LeanTween.scale(target, Vector3.one, duration).setEaseInQuad();
        }

        /// <summary>
        /// Changes the color of a TextMeshProUGUI element on hover.
        /// </summary>
        /// <param name="text">The TextMeshProUGUI component to change.</param>
        /// <param name="targetColor">The target color.</param>
        /// <param name="duration">The duration of the animation.</param>
        public static void onHoverColor(TextMeshProUGUI text, Color targetColor, float duration)
        {
            LeanTween.value(text.gameObject, text.color, targetColor, duration)
                .setOnUpdate((Color val) => text.color = val);
        }

        /// <summary>
        /// Reverts the color of a TextMeshProUGUI element on hover exit.
        /// </summary>
        /// <param name="text">The TextMeshProUGUI component to change.</param>
        /// <param name="originalColor">The original color to revert to.</param>
        /// <param name="duration">The duration of the animation.</param>
        public static void onHoverExitColor(TextMeshProUGUI text, Color originalColor, float duration)
        {
            LeanTween.value(text.gameObject, text.color, originalColor, duration)
                .setOnUpdate((Color val) => text.color = val);
        }

        public static void SlideYUILinear(RectTransform rTransform, float position, float time)
        {
            LeanTween.moveY(rTransform, position, time).setEaseLinear();
        }

        public static void SlideYUILinearWithDisable(RectTransform rTransform, float position, float time, Action OnComplete = null, Action OnStart = null)
        {
            OnStart?.Invoke();
            LeanTween.moveY(rTransform, position, time).setEaseLinear()
                .setOnComplete(() =>
                {
                    OnComplete?.Invoke();
                    rTransform.gameObject.SetActive(false);
                });
        }

        public static void ScaleUI(RectTransform rTransform, Vector2 newPosition, float duration)
        {
            LeanTween.size(rTransform, newPosition, duration).setEaseInQuad();
        }

        public static void ScaleUIWithDisable(RectTransform rTransform, Vector2 newPosition, float duration)
        {
            LeanTween.size(rTransform, newPosition, duration).setEaseInQuad()
                .setOnComplete(() => {rTransform.gameObject.SetActive(false); });
        }

        public static void FadeUI(RectTransform rTransform, float alpha, float duration)
        {
            LeanTween.alpha(rTransform, alpha, duration).setEaseInQuad();
        }

        public static void SlideYUILinearCustomAction(RectTransform rTransform, float position, float time,
            Action OnComplete = null)
        {
            LeanTween.moveY(rTransform, position, time).setEaseLinear()
                .setOnComplete(() => {OnComplete?.Invoke();});
        }
    }
}

