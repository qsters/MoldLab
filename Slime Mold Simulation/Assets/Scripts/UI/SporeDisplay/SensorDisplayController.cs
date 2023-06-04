using ScriptableObjects;
using UnityEngine;

namespace UI.SporeDisplay
{
    public class SensorDisplayController : MonoBehaviour
    {
        [SerializeField] private Transform MiddleSensor;
        [SerializeField] private Transform RightSensor;
        [SerializeField] private Transform LeftSensor;
        [SerializeField] private Transform LeftSensorOrgin;
        [SerializeField] private Transform RightSensorOrgin;
        [SerializeField] private SimulationDataScriptableObject simulationData;

        public void UpdateSensors()
        {
            var sporeAngleDeg = simulationData.sporeSensorOffset;
            var sporeDistanceMagnitude = simulationData.sporeSensorDistance / simulationData.MaxSporeSensorDistance;

            var SensorPosition = new Vector3(0.0f, Mathf.Lerp(4.0f, 10.0f, sporeDistanceMagnitude), 0.0f);

            // Rotate
            LeftSensorOrgin.rotation = Quaternion.Euler(0.0f, 0.0f, sporeAngleDeg);
            RightSensorOrgin.rotation = Quaternion.Euler(0.0f, 0.0f, -sporeAngleDeg);


            LeftSensor.localPosition = SensorPosition;
            RightSensor.localPosition = SensorPosition;
            MiddleSensor.localPosition = SensorPosition;
        }
    }
}