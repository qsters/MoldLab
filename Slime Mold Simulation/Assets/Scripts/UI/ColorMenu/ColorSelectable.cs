using System;
using UI.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ColorMenu
{
    public abstract class ColorSelectable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,
        IPointerExitHandler
    {
        public static ColorSelectable selectedPreset;
        [SerializeField] private Image SelectImage;
        [SerializeField] private bool isSelectable = true;
        private ColorSchemeController controller;

        private void Awake()
        {
            controller = FindObjectOfType<ColorSchemeController>();
        }

        private void Start()
        {
            if (selectedPreset == this)
            {
                UpdateSelected();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isSelectable)
            {
                Select();
                return;
            }

            UpdateSelected();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (selectedPreset == this)
            {
                return;
            }

            HoverColor();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (selectedPreset == this)
            {
                return;
            }

            DeselectColor();
        }

        public void UpdateSelected()
        {
            selectedPreset.DeselectColor();

            Select();
            selectedPreset = this;

            SelectColor();

            Simulation.UpdateSimulation();
            controller.UpdateImage();
        }

        public void SelectColor()
        {
            // Makes it opaque
            var color = SelectImage.color;
            color.a = 1f;
            SelectImage.color = color;
        }

        public void DeselectColor()
        {
            // Makes it clear
            var color = SelectImage.color;
            color.a = 0f;
            SelectImage.color = color;
        }

        public void HoverColor()
        {
            // Makes it grey
            var color = SelectImage.color;
            color.a = 0.5f;
            SelectImage.color = color;
        }


        public abstract void Select();
    }
}