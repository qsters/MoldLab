using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI.SporeDisplay
{
    public class SpeedAndCountDisplayController : MonoBehaviour
    {
        [SerializeField] private Transform SporeHead;
        [SerializeField] private SimulationDataScriptableObject simulaionData;
        [SerializeField] private TMP_Text countText;

        public void UpdateSpeedDisplay()
        {
            var unitSpeed = simulaionData.sporeSpeedPPS / simulaionData.MaxSporeSpeed;

            var scale = 5.0f - unitSpeed * 2.5f;


            var newScale = new Vector3(scale, SporeHead.localScale.y, SporeHead.localScale.y);
            SporeHead.localScale = newScale;
        }

        public void UpdateSporeCountDisplay()
        {
            float displayingNumber;
            string displayingText;
            var sporeCount = simulaionData.sporeCount;
            if (sporeCount >= 1_000_000)
            {
                displayingNumber = Mathf.Round(sporeCount / 1_000_000);
                displayingText = displayingNumber + " Mil";
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