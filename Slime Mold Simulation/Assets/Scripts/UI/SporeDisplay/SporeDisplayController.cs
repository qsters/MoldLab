using UnityEngine;

namespace UI.SporeDisplay
{
    public class SporeDisplayController : MonoBehaviour
    {
        public static SporeDisplayController singleton;
        [SerializeField] private LineDisplayController lineDisplayController;
        [SerializeField] private SensorDisplayController sensorDisplayController;
        [SerializeField] private SpeedAndCountDisplayController speedAndCountController;

        private void Start()
        {
            if (singleton == null)
            {
                singleton = this;
            }
            else
            {
                Debug.LogWarning("SPORE DISPLAY CONTROLLER NOT SINGLETON");
            }

            UpdateAllGraphics();
        }

        public void UpdateLineGraphics()
        {
            lineDisplayController.UpdateLine();
        }

        public void UpdateSensorGraphics()
        {
            sensorDisplayController.UpdateSensors();
        }

        public void UpdateSpeedGraphics()
        {
            speedAndCountController.UpdateSpeedDisplay();
        }

        public void UpdateCountGraphics()
        {
            speedAndCountController.UpdateSporeCountDisplay();
        }

        public void UpdateAllGraphics()
        {
            lineDisplayController.UpdateLine();
            sensorDisplayController.UpdateSensors();
            speedAndCountController.UpdateSpeedDisplay();
            speedAndCountController.UpdateSporeCountDisplay();
        }
    }
}