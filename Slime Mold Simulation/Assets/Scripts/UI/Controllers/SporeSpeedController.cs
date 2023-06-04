using UI.SporeDisplay;
using UnityEngine;

namespace UI.Controllers
{
    public class SporeSpeedController : FieldController
    {
        [SerializeField] private RectTransform speedometerHandle;

        public override void OnSelect()
        {
            // do stuff
        }

        public override void ChangeValue(float amount)
        {
            simulationDataSO.sporeSpeedPPS = Mathf.Max(simulationDataSO.MinSporeSpeed,
                Mathf.Min(simulationDataSO.MaxSporeSpeed,
                    simulationDataSO.sporeSpeedPPS + amount * simulationDataSO.MaxSporeSpeed));

            SporeDisplayController.singleton.UpdateSpeedGraphics();
        }

        public override void UpdateUI()
        {
            var scaledSpeed = simulationDataSO.sporeSpeedPPS / simulationDataSO.MaxSporeSpeed;
            var eulerAngle = new Vector3(0f, 0f, Mathf.Lerp(120f, -60f, scaledSpeed));
            speedometerHandle.rotation = Quaternion.Euler(eulerAngle);
        }
    }
}