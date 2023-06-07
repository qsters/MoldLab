using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace UI.ColorMenu
{
    public class ColorMenuManager : MonoBehaviour
    {
        [SerializeField] private RectTransform thisTransform;

        private void Update()
        {
            UIHandler.ResetFading();
        }

        public void TestDeselect(InputAction.CallbackContext context)
        {
            if (!context.started || !gameObject.activeSelf ||
            PickerManager.pickingColor || AdPopup.singleton.isOpen)
            {
                return;
            }

            
            if (!RectTransformUtility.RectangleContainsScreenPoint(
                    thisTransform,
#if (UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)
                    Mouse.current.position.ReadValue(),
#elif UNITY_IOS || UNITY_ANDROID
                    Touch.activeFingers[0].screenPosition,
#endif
                    null))
            {
                Deactivate();
            }
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}