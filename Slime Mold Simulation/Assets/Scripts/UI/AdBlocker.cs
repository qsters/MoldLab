using System;
using System.Collections;
using System.Collections.Generic;
using Advertisements;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class AdBlocker : MonoBehaviour
{
    [SerializeField] public bool coversPresets;
    
    private void Awake()
    {
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        this.gameObject.SetActive(false);
        return;
#endif

        if (Simulation.singleton.simulationDataSO.adsPaid)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        if (AdsInitializer.initialized)
        {
            RewardedAdsButton.singleton.LoadAd();
        }

        AdPopup.singleton.Popup(this);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
}