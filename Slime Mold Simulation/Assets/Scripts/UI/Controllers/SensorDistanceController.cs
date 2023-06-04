using UI.SporeDisplay;
using UnityEngine;

namespace UI.Controllers
{
    public class SensorDistanceController : FieldController
    {
        [SerializeField] private RectTransform sensor;

        public override void OnSelect()
        {
            //
        }

        public override void ChangeValue(float amount)
        {
            simulationDataSO.sporeSensorDistance = Mathf.Max(simulationDataSO.MinSporeSensorDistance,
                Mathf.Min(simulationDataSO.MaxSporeSensorDistance,
                    simulationDataSO.sporeSensorDistance + amount * simulationDataSO.MaxSporeSensorDistance));

            SporeDisplayController.singleton.UpdateSensorGraphics();
        }

        public override void UpdateUI()
        {
            var scaledDistance = simulationDataSO.sporeSensorDistance / simulationDataSO.MaxSporeSensorDistance;
            var position = sensor.localPosition;

            position.y = Mathf.Lerp(-210.0f, -155.0f, scaledDistance);
            sensor.localPosition = position;
        }
    }
}