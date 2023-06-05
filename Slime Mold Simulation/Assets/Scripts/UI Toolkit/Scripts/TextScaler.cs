using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TextScaler : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    private VisualElement root;

    private void Start()
    {
        root = document.rootVisualElement;

        int fontSize = 20;

        if (Screen.height < 1000)
        {
            fontSize = 15;
        }
        else if (Screen.height >= 1000 && Screen.height < 1300)
        {
            fontSize = 20;
        }
        else if (Screen.height >= 1300 && Screen.height < 1700)
        {
            fontSize = 25;
        }
        else if (Screen.height >= 1700)
        {
            fontSize = 30;
        }

        List<VisualElement> result = root.Query("ControllerText").ToList();
        foreach (var visualElement in result)
        {
            visualElement.style.fontSize = new StyleLength(fontSize);
        }
    }
}