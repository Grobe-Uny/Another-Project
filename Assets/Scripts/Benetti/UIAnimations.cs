using TMPro;
using UnityEngine;

namespace Benetti
{
    public class UIAnimations 
    {

        public static void onHoverEnter(RectTransform target, float scale, float duration)
        {
            LeanTween.scale(target, Vector3.one * scale, duration).setEaseOutQuad();
        }

        public static void onHoverExit(RectTransform target, float duration)
        {
            LeanTween.scale(target, Vector3.one, duration).setEaseInQuad();
        }

        public static void onHoverColor(TextMeshProUGUI text, Color targetColor, float duration)
        {
            LeanTween.value(text.gameObject, text.color, targetColor, duration)
                .setOnUpdate((Color val) => text.color = val);
        }
    }
}

