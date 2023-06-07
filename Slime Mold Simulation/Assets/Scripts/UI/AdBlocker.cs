using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class AdBlocker : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        this.gameObject.SetActive(false);
#endif
    }

    public void OnClick()
    {
        AdPopup.singleton.Popup(this);
    }
}
