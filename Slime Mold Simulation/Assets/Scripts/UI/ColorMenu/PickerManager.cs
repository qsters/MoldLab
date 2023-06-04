using HSVPicker;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace UI.ColorMenu
{
    public class PickerManager : MonoBehaviour
    {
        public static bool pickingColor;
        [SerializeField] private RectTransform pickerTransform;
        [SerializeField] public ColorPicker picker;
        private ColorChooser currentChoice;

        private void Update()
        {
            currentChoice.Color = picker.CurrentColor;
        }

        public void TestUnfocused(InputAction.CallbackContext context)
        {
            if (!context.started || !gameObject.activeSelf)
            {
                return;
            }

            if (!RectTransformUtility.RectangleContainsScreenPoint(
                    pickerTransform,
#if (UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)
                    Mouse.current.position.ReadValue(),
#elif UNITY_IOS || UNITY_ANDROID
                    Touch.activeFingers[0].screenPosition,
#endif

                    null))
            {
                Deactivate();
                pickingColor = false;
            }
        }

        public void Activate(ColorChooser choice)
        {
            pickerTransform.gameObject.SetActive(true);
            currentChoice = choice;
            picker.CurrentColor = choice.Color;

            pickingColor = true;
        }

        public void Deactivate()
        {
            pickerTransform.gameObject.SetActive(false);
        }
    }
}