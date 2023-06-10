using System;
using Helpers;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class SporeDisplayHandler : MonoBehaviour
    {
        private static CanvasGroup display;
        public static SporeDisplayHandler singleton;
        private void Awake()
        {
            singleton = this;
            display = GetComponent<CanvasGroup>();
        }

        public static void Fade(float alpha, float duration)
        {
            singleton.StopAllCoroutines();
            if (duration <= 0f)
            {
                display.alpha = alpha;
            }
            else
            {
                singleton.StartCoroutine(UIHelper.FadeCanvasGroup(display, alpha, duration));
            }
        }
    }
}
