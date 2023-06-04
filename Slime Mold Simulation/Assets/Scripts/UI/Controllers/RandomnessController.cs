using UI.SporeDisplay;
using UnityEngine;

namespace UI.Controllers
{
    public class RandomnessController : FieldController
    {
        [SerializeField] private LineRenderer randomLine;
        private Vector3[] originalPositions;
        private float randomOffset;

        private void Awake()
        {
            originalPositions = new Vector3[randomLine.positionCount];
            randomLine.GetPositions(originalPositions);
        }

        public override void OnSelect()
        {
            //
        }


        public override void ChangeValue(float amount)
        {
            simulationDataSO.randomnessScale = Mathf.Max(simulationDataSO.MinRandomnessScale,
                Mathf.Min(simulationDataSO.MaxRandomnessScale,
                    simulationDataSO.randomnessScale + amount * simulationDataSO.MaxRandomnessScale));

            SporeDisplayController.singleton.UpdateLineGraphics();
        }

        public override void UpdateUI()
        {
            var newPositions = new Vector3[originalPositions.Length];
            var wasSetMax = false;

            Random.seed = (int)(simulationDataSO.randomnessScale * 100f) + 50;
            for (var i = 0; i < newPositions.Length; i++)
            {
                var scaledRandomness = simulationDataSO.randomnessScale / simulationDataSO.MaxRandomnessScale;

                var offset = Random.Range(-1f, 1f) * scaledRandomness;


                newPositions[i] = originalPositions[i];
                if (Random.Range(0f, 20f) <= 3f && !wasSetMax)
                {
                    newPositions[i].y += scaledRandomness * 4f * (Random.value < 0.5f ? -1f : 1f);
                    wasSetMax = true;
                }
                else
                {
                    wasSetMax = false;
                    newPositions[i].y += offset * Random.Range(0f, 4f);
                }
            }

            randomLine.SetPositions(newPositions);
        }
    }
}