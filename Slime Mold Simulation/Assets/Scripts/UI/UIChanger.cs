using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIChanger : MonoBehaviour
{
    [FormerlySerializedAs("ExplanationPointer")] [SerializeField]
    private Image explanationPointer;

    [FormerlySerializedAs("ExplanationPointerSprites")] [SerializeField]
    private Sprite[] explanationPointerSprites;

    [SerializeField] private RectTransform controlPanel;

    void Start()
    {
        
#if (UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)
        explanationPointer.sprite = explanationPointerSprites[0];
        controlPanel.anchoredPosition = new Vector2(-80, 0f);
#elif UNITY_IOS || UNITY_ANDROID
        explanationPointer.sprite = explanationPointerSprites[1];
        controlPanel.anchoredPosition = new Vector2(-205f, 0f);

        if (SystemInfo.deviceModel.ToLower().Contains("ipad"))
        {
            controlPanel.anchoredPosition = new Vector2(-80f, 0f);
        }
#endif
        
    }
}