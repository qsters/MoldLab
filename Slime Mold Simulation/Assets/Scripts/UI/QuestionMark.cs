using Helpers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class QuestionMark : MonoBehaviour, IPointerClickHandler
    {
        private static CanvasGroup display;
        public static QuestionMark singleton;

        private void Awake()
        {
            singleton = this;
            display = GetComponent<CanvasGroup>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SporeExplanationPopup.singleton.ShowPopup();
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