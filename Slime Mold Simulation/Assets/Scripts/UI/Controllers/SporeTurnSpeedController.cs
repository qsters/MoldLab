using UI.SporeDisplay;
using UnityEngine;

namespace UI.Controllers
{
    public class SporeTurnSpeedController : FieldController
    {
        [SerializeField] private LineRenderer renderingLine;

        public override void OnSelect()
        {
            //
        }

        public override void ChangeValue(float amount)
        {
            simulationDataSO.sporeTurnSpeedRPS = Mathf.Max(simulationDataSO.MinSporeTurnSpeed,
                Mathf.Min(simulationDataSO.MaxSporeTurnSpeed,
                    simulationDataSO.sporeTurnSpeedRPS + amount * simulationDataSO.MaxSporeTurnSpeed));

            SporeDisplayController.singleton.UpdateLineGraphics();
        }

        public override void UpdateUI()
        {
            //  
        }

        public void CopyLine(LineRenderer referenceLine)
        {
            renderingLine.positionCount = referenceLine.positionCount;
            var newPositions = new Vector3[renderingLine.positionCount];
            referenceLine.GetPositions(newPositions);
            renderingLine.SetPositions(newPositions);
        }
    }
}