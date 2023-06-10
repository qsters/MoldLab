using System;
using Helpers;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tutorial
{
    public class TutorialSkipper : MonoBehaviour
    {
        [SerializeField] private CanvasGroup UIGroup;
        [SerializeField] private RadialProgressBar progressBar;

        [SerializeField] private Image SkippingSymbol;
        [SerializeField] private Sprite[] SkippingSprites;

        private float spaceTimer;
        private float timer = 3f;

        private bool UIActive = true;

        private void Start()
        {
#if (UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)
            SkippingSymbol.sprite = SkippingSprites[0];
            SkippingSymbol.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-86.137f, 9.6938f, 0f);
            SkippingSymbol.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(123.6276f, 61.0716f);

#elif UNITY_IOS || UNITY_ANDROID
            EnhancedTouchSupport.Enable();
            SkippingSymbol.sprite = SkippingSprites[1];
            SkippingSymbol.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-86.137f, 18.647f, 0f);
            SkippingSymbol.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(123.6276f, 117.7528f);
#endif
        }

        private void Update()
        {
            if (!UIActive)
            {
                if (
#if (UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)
                Keyboard.current.spaceKey.isPressed
#elif UNITY_IOS || UNITY_ANDROID
                    Touch.activeFingers.Count > 0
#endif
                )
                {
                    StopAllCoroutines();
                    StartCoroutine(UIHelper.FadeCanvasGroup(UIGroup, 1f, 0.1f));
                    UIActive = true;
                    timer = 3f;
                }
            }
            else
            {
                timer -= Time.deltaTime;

                if (timer <= 0f)
                {
                    StopAllCoroutines();
                    StartCoroutine(UIHelper.FadeCanvasGroup(UIGroup, 0f, 0.2f));
                    UIActive = false;
                }
            }

            if (
#if (UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)
                Keyboard.current.spaceKey.isPressed
#elif UNITY_IOS || UNITY_ANDROID
                Touch.activeFingers.Count > 0
#endif
            )
            {
                spaceTimer += Time.deltaTime;
                if (spaceTimer >= 1.5f)
                {
                    SceneManager.LoadScene(1);
                }
            }
            else
            {
                spaceTimer -= 3 * Time.deltaTime;
                if (spaceTimer <= 0f)
                {
                    spaceTimer = 0f;
                }
            }

            progressBar.SetPercent(Mathf.Lerp(spaceTimer / 1.5f, 1f, spaceTimer / 1.5f));
        }
    }
}