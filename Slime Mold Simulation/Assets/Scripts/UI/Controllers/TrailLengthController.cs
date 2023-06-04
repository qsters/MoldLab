using UI.SporeDisplay;
using UnityEngine;

namespace UI.Controllers
{
    public class TrailLengthController : FieldController
    {
        [SerializeField] private LineRenderer fadingLine;

        public override void OnSelect()
        {
            //
        }

        public override void ChangeValue(float amount)
        {
            simulationDataSO.trailLengthPixels = Mathf.Max(simulationDataSO.MinSporeTrailLength,
                Mathf.Min(simulationDataSO.MaxSporeTrailLength,
                    simulationDataSO.trailLengthPixels + amount * simulationDataSO.MaxSporeTrailLength));

            SporeDisplayController.singleton.UpdateLineGraphics();
        }

        public override void UpdateUI()
        {
            var scaledLength = simulationDataSO.trailLengthPixels / simulationDataSO.MaxSporeTrailLength;

            var whiteKeyPosition = Mathf.Lerp(0.4f, 1.0f, scaledLength);
            var clearKeyPosition = Mathf.Min(1.0f, whiteKeyPosition + 0.1f);

            var gradient = new Gradient();

            gradient.SetKeys(
                new[] { new(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
                new[]
                {
                    new GradientAlphaKey(1.0f, 0.0f),
                    new GradientAlphaKey(1.0f, whiteKeyPosition),
                    new GradientAlphaKey(0.0f, clearKeyPosition)
                }
            );
            fadingLine.colorGradient = gradient;
        }
    }
}