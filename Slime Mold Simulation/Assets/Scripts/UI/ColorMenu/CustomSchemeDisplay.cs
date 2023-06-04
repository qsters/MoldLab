using Helpers;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

namespace UI.ColorMenu
{
    public class CustomSchemeDisplay : ColorSelectable
    {
        [SerializeField] private RawImage image;
        [SerializeField] private ComputeShader displayShader;
        [SerializeField] private Image choice1;
        [SerializeField] private Image choice2;
        public RenderTexture displayTexture;
        private CustomDisplayData data;
        private ComputeBuffer dataBuffer;
        private ColorSchemeMode lastSelectedMode;

        private void Start()
        {
            lastSelectedMode = ColorSchemeMode.Choice1;
            if (displayTexture != null)
            {
                displayTexture.Release();
            }

            displayTexture = new RenderTexture(500, 500, 1);
            displayTexture.enableRandomWrite = true;
            displayTexture.graphicsFormat = GraphicsFormat.R16G16B16A16_SFloat;

            displayTexture.autoGenerateMips = false;
            displayTexture.Create();


            displayTexture.wrapMode = TextureWrapMode.Clamp;
            displayTexture.filterMode = FilterMode.Point;

            image.texture = displayTexture;

            displayShader.SetTexture(0, "displayTexture", displayTexture);
            UpdateAndDispatch();
        }

        private void OnDestroy()
        {
            dataBuffer.Release();
        }

        public void ChangeMode(ColorSchemeMode mode)
        {
            lastSelectedMode = mode;

            UpdateAndDispatch();
        }

        private void UpdateAndDispatch()
        {
            data = new CustomDisplayData
            {
                colorModeIndex = (int)lastSelectedMode,
                ColorChoice1 = choice1.color,
                ColorChoice2 = choice2.color
            };
            if (choice1.color.a < 1f || choice2.color.a < 1f)
            {
                Debug.LogWarning("Chosen colors alphas not 1!!!");
            }

            ComputeHelper.CreateAndSetBuffer(ref dataBuffer, new[] { data }, displayShader, "displayData");
            ComputeHelper.Dispatch(displayShader, displayTexture.width, displayTexture.height);
        }

        public override void Select()
        {
            Simulation.simulationData.colorMode = lastSelectedMode;
        }

        private struct CustomDisplayData
        {
            public int colorModeIndex;
            public Vector4 ColorChoice1;
            public Vector4 ColorChoice2;
        }
    }
}