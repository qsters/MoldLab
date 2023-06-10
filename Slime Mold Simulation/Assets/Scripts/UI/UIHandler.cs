using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHandler : MonoBehaviour
    {
        private const float DISPLAY_SELECTED_ALPHA = 1f;
        private const float DISPLAY_NORMAL_ALPHA = 0.8f;
        private const float DISPLAY_FADED_ALPHA = 0.6f;

        private const float BACKGROUND_SELECTED_ALPHA = 1f;
        private const float BACKGROUND_NORMAL_ALPHA = 1f;
        private const float BACKGROUND_FADED_ALPHA = 0f;

        private const float FADING_TIME = 5f;
        private static float fadingTimer = FADING_TIME;
        public static bool isFaded;
        public static bool isFading = true;

        private static FieldController[] fieldControllers;

        private void Start()
        {
            // Find all FieldController components in the scene
            fieldControllers = FindObjectsOfType<FieldController>();

            // Check if any component matches FieldController.selectedField
            foreach (FieldController controller in fieldControllers)
            {
                if (controller.gameObject.name == "Sensor Distance Controller")
                {
                    FieldController.selectedField = controller;
                    controller.GetComponent<Button>().interactable = false;
                }
            }
        }

        private void Update()
        {
            if (isFading)
            {
                fadingTimer -= Time.deltaTime;

                if (fadingTimer <= 0f)
                {
                    isFaded = true;
                    isFading = false;
                    FadeUI();
                }
            }
        }

        public static void ResetFading()
        {
            if (isFaded)
            {
                UnfadeUI();
            }

            isFading = true;
            isFaded = false;
            fadingTimer = FADING_TIME;
        }

        private static void UnfadeUI()
        {
            var durationToFade = 0.2f;

            foreach (var controller in fieldControllers)
                if (controller == FieldController.selectedField)
                {
                    controller.FadeTheField(BACKGROUND_SELECTED_ALPHA, durationToFade, DISPLAY_SELECTED_ALPHA,
                        durationToFade);
                }
                else
                {
                    controller.FadeTheField(BACKGROUND_NORMAL_ALPHA, durationToFade, DISPLAY_NORMAL_ALPHA,
                        durationToFade);
                }

            SporeDisplayHandler.Fade(1.0f, durationToFade);
            QuestionMark.Fade(1f, durationToFade);
        }

        private static void FadeUI()
        {
            var durationToFade = 1f;

            foreach (var controller in fieldControllers)
                if (controller == FieldController.selectedField)
                {
                    controller.FadeTheField(BACKGROUND_FADED_ALPHA, durationToFade, DISPLAY_FADED_ALPHA,
                        durationToFade);
                }
                else
                {
                    controller.FadeTheField(0f, durationToFade, 0f, durationToFade);
                }

            SporeDisplayHandler.Fade(DISPLAY_FADED_ALPHA, durationToFade);
            QuestionMark.Fade(DISPLAY_FADED_ALPHA, durationToFade);
        }
        
        public static void SetAllButtonsInteractable()
        {
            foreach (FieldController controller in GameObject.FindObjectsOfType<FieldController>())
            {
                controller.GetComponent<Button>().interactable = true;
            }
        }
    }
}