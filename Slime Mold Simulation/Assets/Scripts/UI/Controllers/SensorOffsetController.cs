using UI.SporeDisplay;
using UnityEngine;

namespace UI.Controllers
{
    public class SensorOffsetController : FieldController
    {
        [SerializeField] private RectTransform rightSensorPivot;
        [SerializeField] private RectTransform leftSensorPivot;

        public override void OnSelect()
        {
            //
        }

        public override void ChangeValue(float amount)
        {
            simulationDataSO.sporeSensorOffset = Mathf.Max(simulationDataSO.MinSporeSensorAngle,
                Mathf.Min(simulationDataSO.MaxSporeSensorAngle,
                    simulationDataSO.sporeSensorOffset + amount * simulationDataSO.MaxSporeSensorAngle));

            SporeDisplayController.singleton.UpdateSensorGraphics();
        }

        public override void UpdateUI()
        {
            leftSensorPivot.rotation = Quaternion.Euler(0f, 0f, simulationDataSO.sporeSensorOffset);
            rightSensorPivot.rotation = Quaternion.Euler(0f, 0f, -simulationDataSO.sporeSensorOffset);
        }
    }
}