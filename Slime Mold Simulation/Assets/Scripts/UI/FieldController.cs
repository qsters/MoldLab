using Helpers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum SimulationFields
    {
        SporeCount,
        SporeSpeed,
        SporeTurnSpeed,
        SporeSensorDistance,
        SporeSensorAngle,
        Randomness,
        TrailLength,
        AttractionAndRepulsion,
        ColorScheme
    }

    public abstract class FieldController : MonoBehaviour
    {
        public static FieldController selectedField;

        [SerializeField] public bool isSelectable = true;
        [SerializeField] public SimulationDataScriptableObject simulationDataSO;

        [SerializeField] private CanvasGroup backgroundGroup;
        [SerializeField] private CanvasGroup displayGroup;

        private void Awake()
        {
            if (backgroundGroup == null || displayGroup == null)
            {
                Debug.LogWarning("Children don't contain CanvasGroup: FIELDCONTROLLER");
            }
        }

        private void Start()
        {
            UpdateUI();
        }

        public void OnPointerClick()
        {
            OnSelect();

            if (isSelectable && selectedField != this)
            {
                UIHandler.SetAllButtonsInteractable();
                GetComponent<Button>().interactable = false;

                selectedField = this;
            }

            UIHandler.ResetFading();
            Simulation.UpdateSimulation();
            UpdateUI();
        }

        public void DragDetected(float delta)
        {
            ChangeValue(delta);
            Simulation.UpdateSimulation();
            UpdateUI();
        }

        public void FadeTheField(float newBackgroundAlpha, float backgroundDuration, float newDisplayAlpha,
            float displayDuration)
        {
            StopAllCoroutines();
            if (backgroundDuration <= 0f)
            {
                backgroundGroup.alpha = newBackgroundAlpha;
            }
            else
            {
                StartCoroutine(UIHelper.FadeCanvasGroup(backgroundGroup, newBackgroundAlpha, backgroundDuration));
            }

            if (displayDuration <= 0f)
            {
                displayGroup.alpha = newDisplayAlpha;
            }
            else
            {
                StartCoroutine(UIHelper.FadeCanvasGroup(displayGroup, newDisplayAlpha, displayDuration));
            }
        }

        public abstract void OnSelect();
        public abstract void ChangeValue(float amount);
        public abstract void UpdateUI();
    }
}