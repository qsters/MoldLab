using ScriptableObjects;
using UI.Controllers;
using UnityEngine;

namespace UI.SporeDisplay
{
    public class LineDisplayController : MonoBehaviour
    {
        [SerializeField] private LineRenderer referenceLine;
        [SerializeField] private LineRenderer displayLine;
        [SerializeField] private SporeTurnSpeedController UILineController;
        [SerializeField] private SimulationDataScriptableObject simulationData;

        public void UpdateLine()
        {
            UpdateTurnAmountDisplay();
            BezierLineMaker.singleton.UpdateLine();

            UILineController.CopyLine(displayLine);

            UpdateFadeAmountDisplay();
            UpdateRandomAmountDisplay();
        }

        private void UpdateTurnAmountDisplay()
        {
            for (var i = 3; i < 12; i++)
            {
                var position = referenceLine.GetPosition(i);

                var magnitude = simulationData.sporeTurnSpeedRPS / simulationData.MaxSporeTurnSpeed * 3.0f *
                                (i % 2 == 0 ? 1 : -1);

                position.x = magnitude;

                referenceLine.SetPosition(i, position);
            }

            BezierLineMaker.singleton.smoothingLength =
                simulationData.sporeTurnSpeedRPS / simulationData.MaxSporeTurnSpeed * 3.0f;
        }

        private void UpdateFadeAmountDisplay()
        {
            var gradient = new Gradient();

            var magnitude = simulationData.trailLengthPixels / simulationData.MaxSporeTrailLength;

            // Scaled between 0.2 and 1 when there is no curve in line, and between 0.02 and 1 when there is a full curve, linearly
            var whiteKeyPosition = Mathf.Lerp(magnitude * 0.8f + 0.2f, magnitude * 0.98f + 0.02f,
                simulationData.sporeTurnSpeedRPS / simulationData.MaxSporeTurnSpeed);

            var clearKeyPosition = Mathf.Min(1.0f, whiteKeyPosition + 0.15f);

            gradient.SetKeys(
                new[] { new(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
                new[]
                {
                    new GradientAlphaKey(0.0f, 0.025f), new GradientAlphaKey(1.0f, 0.026f),
                    new GradientAlphaKey(1.0f, whiteKeyPosition), new GradientAlphaKey(
                        simulationData.trailLengthPixels < simulationData.MaxSporeTrailLength ? 0.0f : 1.0f,
                        clearKeyPosition)
                }
            );

            displayLine.colorGradient = gradient;
        }

        private void UpdateRandomAmountDisplay()
        {
            var segmentsPerBeziere = BezierLineMaker.singleton.segmentCount;
            var positions = new Vector3[displayLine.positionCount];
            displayLine.GetPositions(positions);

            var xOffset = 101.0f;
            var yOffset = -101.0f;

            for (var i = segmentsPerBeziere * 2; i < positions.Length; i++)
            {
                var position = positions[i];
                var xPosition = position.x + xOffset;
                var yPosition = position.y + yOffset;

                var direction = positions[i - 1] - position;
                var perpendicularDirection = new Vector3(-direction.y, direction.x, 0.0f).normalized;

                var magnitude = (Mathf.PerlinNoise(xPosition, yPosition) - 0.5f) *
                                (simulationData.randomnessScale / simulationData.MaxRandomnessScale) * 0.4f;

                var newPosition = positions[i] + perpendicularDirection * magnitude;

                displayLine.SetPosition(i, newPosition);
            }
        }
    }
}