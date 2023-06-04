using TMPro;
using UI.SporeDisplay;
using UnityEngine;

namespace UI
{
    public class SporeCountController : FieldController
    {
        [SerializeField] private TMP_Text countText;

        public override void OnSelect()
        {
            // Do stuff with animation
        }

        public override void ChangeValue(float amount)
        {
            simulationDataSO.sporeCount = Mathf.Max(simulationDataSO.MinSporeCount,
                Mathf.Min(simulationDataSO.MaxSporeCount,
                    simulationDataSO.sporeCount + (int)(amount * simulationDataSO.MaxSporeCount)));

            SporeDisplayController.singleton.UpdateCountGraphics();
        }

        public override void UpdateUI()
        {
            float displayingNumber;
            string displayingText;
            var sporeCount = simulationDataSO.sporeCount;
            if (sporeCount >= 1_000_000)
            {
                displayingNumber = Mathf.Round(sporeCount / 1_000_000);
                displayingText = displayingNumber + " M";
            }
            else if (sporeCount >= 100_000)
            {
                displayingNumber = Mathf.Round(sporeCount / 1_000);
                displayingText = displayingNumber + " K";
            }
            else
            {
                displayingText = sporeCount.ToString();
            }

            countText.text = displayingText;
        }
    }
}