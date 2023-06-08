using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace UI
{
    public class AdPopup : MonoBehaviour
    {
        public AdBlocker currentBlocker;
        public static AdPopup singleton;
        public bool isOpen;

        
        private void Awake()
        {
            if (singleton != null)
            {
                Debug.LogWarning("NOT A SINGLETON: ADPOPUP");
            }
            else
            {
                singleton = this;
            }
            
            gameObject.SetActive(false);
        }
        
        public void RestoringSucceeded(Boolean field1, String field2)
        {
            Debug.Log("this is a thing");
        }

        public void Popup(AdBlocker orgin)
        {
            gameObject.SetActive(true);
            isOpen = true;
            currentBlocker = orgin;
        }

        public void TestDeselect(InputAction.CallbackContext context)
        {
            
            if (!context.started || !gameObject.activeSelf)
            {
                return;
            }
            
            if (!RectTransformUtility.RectangleContainsScreenPoint(
                    GetComponent<RectTransform>(),
                    Touch.activeFingers[0].screenPosition,
                    null))
            {
                gameObject.SetActive(false);
                isOpen = false;
            }
        }

        public void OnRemoveAdButton()
        {
            gameObject.SetActive(false);
            PayPopup.singleton.gameObject.SetActive(true);
        }
        
        
        public void AdWatched()
        {
            if (currentBlocker.coversPresets)
            {
                foreach (var adBlocker in FindObjectsOfType<AdBlocker>())
                {
                    if (adBlocker.coversPresets)
                    {
                        adBlocker.Hide();
                    }
                }
            }
            else
            {
                currentBlocker.Hide();
            }
        }
    }
}