using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace UI
{
    public class SporeExplanationPopup : MonoBehaviour
    {
        public static SporeExplanationPopup singleton;
        [SerializeField] private UIDocument _document;
        private VisualElement root;
        private List<VisualElement> textElements;
        private VisualElement draggingHand;
        private VisualElement mainPanel;

        private void Start()
        {
            singleton = this;
            root = _document.rootVisualElement;
            textElements = root.Query(className: "TextBoxScalable").ToList();
            draggingHand = root.Q("DraggingHand");
            mainPanel = root.Q("MainPanel");


            int fontSize = 30;
            float aspectRatio = (float)Screen.width / (float)Screen.height;

            if (aspectRatio < 1.5f)
            {
                fontSize = 60;

                if (Screen.height > 2000)
                {
                    fontSize = 75;
                }
            }
            else
            {
                if (Screen.width < 1800)
                {
                    fontSize = 30;
                }
                else if (Screen.width >= 1800 && Screen.width < 2500)
                {
                    fontSize = 40;
                }
                else if (Screen.width >= 2500)
                {
                    fontSize = 60;
                }
            }

            foreach (var visualElement in textElements)
            {
                visualElement.style.fontSize = new StyleLength(fontSize);
            }
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

            draggingHand.style.translate =
                new StyleTranslate(new Translate(Mathf.Sin(Time.timeSinceLevelLoad * 2f) * 100f, 0));
        }

        private void HidePopup()
        {
            mainPanel.style.opacity = 0;
        }

        public void ShowPopup()
        {
            mainPanel.style.opacity = 1;
        }
    }
}