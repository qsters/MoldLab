using UI.ColorMenu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Controllers
{
    public class ColorSchemeController : FieldController
    {
        [SerializeField] private RawImage image;
        [SerializeField] private ColorMenuManager menuManager;
        [SerializeField] private CustomSchemeDisplay display;
        [SerializeField] private Texture2D[] textures;

        private void Start()
        {
            UpdateImage();
        }

        public override void OnSelect()
        {
            selectedField.gameObject.GetComponent<Button>().Select();
            menuManager.Activate();
        }

        public override void ChangeValue(float amount)
        {
            //
        }

        public override void UpdateUI()
        {
            //
        }

        public void UpdateImage()
        {
            var mode = Simulation.simulationData.colorMode;
            var scheme = Simulation.simulationData.colorScheme;

            if (mode == ColorSchemeMode.Preset)
            {
                image.texture = textures[(int)scheme];
            }
            else
            {
                image.texture = display.displayTexture;
            }
        }
    }
}