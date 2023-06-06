using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class UIChanger : MonoBehaviour
{
    [SerializeField] private RectTransform controlPanel;
    [SerializeField] private UIDocument _document;
    [SerializeField] private Texture2D mouseTexture;
    [SerializeField] private Texture2D SpaceBar;
    [SerializeField] private Texture2D ShiftKey;
    [SerializeField] private Texture2D CKey;

    private VisualElement root;
    private VisualElement draggingHand;
    private List<VisualElement> keyElements;
    private List<Label> textElements;


    private void Awake()
    {
#if (UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)
        root = _document.rootVisualElement;
        draggingHand = root.Q("DraggingHand");
        draggingHand.style.backgroundImage = new StyleBackground(mouseTexture);
        keyElements = root.Query(className: "KeyImages").ToList();
        textElements = root.Query<Label>(className: "TextEdit").ToList();
        
        keyElements[0].style.backgroundImage = new StyleBackground(ShiftKey);
        keyElements[0].style.width = new StyleLength(100);
        textElements[0].text = "Hold";
        keyElements[1].style.backgroundImage = new StyleBackground(SpaceBar);
        keyElements[1].style.width = new StyleLength(100);
        textElements[1].text = "Press";
        keyElements[2].style.backgroundImage = new StyleBackground(CKey);
        keyElements[2].style.width = new StyleLength(80);
        textElements[2].text = "Press";
        
        controlPanel.anchoredPosition = new Vector2(-80, 0f);
#elif UNITY_IOS || UNITY_ANDROID
        // explanationPointer.sprite = explanationPointerSprites[1];
        controlPanel.anchoredPosition = new Vector2(-205f, 0f);

        if (SystemInfo.deviceModel.ToLower().Contains("ipad"))
        {
            controlPanel.anchoredPosition = new Vector2(-80f, 0f);
        }
#endif
    }
}