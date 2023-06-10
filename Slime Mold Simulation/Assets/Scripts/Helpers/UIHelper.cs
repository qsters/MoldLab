using System.Collections;
using UnityEngine;

namespace Helpers
{
    public class UIHelper
    {
        
        
        public static IEnumerator FadeCanvasGroup(CanvasGroup canvasGroupToFade, float targetAlpha, float duration)
        {
            // Get the initial alpha value of the canvas group
            var startAlpha = canvasGroupToFade.alpha;

            // Calculate the difference in alpha values
            var alphaDifference = Mathf.Abs(startAlpha - targetAlpha);

            // Calculate the alpha change per second
            var alphaChangePerSecond = alphaDifference / duration;

            // Determine whether the alpha should be increasing or decreasing
            var direction = targetAlpha > startAlpha ? 1 : -1;

            // Fade the canvas group until the target alpha is reached
            while (Mathf.Abs(canvasGroupToFade.alpha - targetAlpha) > 0.01f)
            {
                // Calculate the new alpha value
                var newAlpha = canvasGroupToFade.alpha + alphaChangePerSecond * direction * Time.deltaTime;

                // Clamp the new alpha value to the range of 0 to 1
                newAlpha = Mathf.Clamp01(newAlpha);

                // Set the new alpha value on the canvas group
                canvasGroupToFade.alpha = newAlpha;

                // Wait for the next frame
                yield return null;
            }

            canvasGroupToFade.alpha = targetAlpha;
        }
    }
}
