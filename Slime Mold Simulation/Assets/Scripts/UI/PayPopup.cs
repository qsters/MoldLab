using System;
using UI;
using UnityEngine.Purchasing;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PayPopup : MonoBehaviour
{
    public static PayPopup singleton;

    private void Awake()
    {
        singleton = this;
        gameObject.SetActive(false);
    }

    public void RemoveAds()
    {
        Simulation.singleton.simulationDataSO.adsPaid = true;
        foreach (var adBlocker in FindObjectsOfType<AdBlocker>())
        {
            adBlocker.Hide();
        }
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
            AdPopup.singleton.isOpen = false;
        }
    }
}
