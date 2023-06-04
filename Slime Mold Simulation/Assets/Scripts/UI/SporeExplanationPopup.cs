using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace UI
{
    public class SporeExplanationPopup : MonoBehaviour
    {
        public static SporeExplanationPopup singleton;
        [SerializeField] private CanvasGroup popup;
        [SerializeField] private RectTransform mouseTransform;

        private void Start()
        {
            singleton = this;
            popup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (Time.timeSinceLevelLoad > 1f)
            {
#if (UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)
                if (Mouse.current.leftButton.isPressed)
                {
                    HidePopup();
                }
#elif UNITY_IOS || UNITY_ANDROID
                if (Touch.activeFingers.Count > 0)
                {
                    HidePopup();
                }
#endif
            }

            mouseTransform.localPosition = new Vector3(-600f + Mathf.Sin(Time.timeSinceLevelLoad * 2f) * 100f, 200f);
        }

        public void Fade(float alpha, float duration)
        {
            singleton.StopAllCoroutines();
            if (duration <= 0f)
            {
                singleton.popup.alpha = alpha;
            }
            else
            {
                singleton.StartCoroutine(UIHelper.FadeCanvasGroup(popup, alpha, duration));
            }
        }

        public void ShowPopup()
        {
            popup.interactable = true;
            enabled = true;

            singleton.Fade(1f, 0.2f);
        }

        public void HidePopup()
        {
            popup.interactable = false;
            enabled = false;

            singleton.Fade(0f, 0.2f);
        }
    }
}