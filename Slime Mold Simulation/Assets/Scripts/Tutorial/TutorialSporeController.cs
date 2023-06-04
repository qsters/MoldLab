using PathCreation;
using UnityEngine;

namespace Tutorial
{
    [ExecuteInEditMode]
    public class TutorialSporeController : MonoBehaviour
    {
        [SerializeField] public float pathDistance;
        [SerializeField] public PathCreator pathCreator;
        [SerializeField] public Transform head;
        [SerializeField] public LineRenderer tail;
        [SerializeField] public Transform RightSensorOrigin;
        [SerializeField] public Transform LeftSensorOrigin;
        [SerializeField] public Transform RightSensor;
        [SerializeField] public Transform LeftSensor;
        [SerializeField] public Transform CenterSensor;

        [Header("Spore Info")] [SerializeField]
        public float trailLength = 10f;

        public int trailVertexCount = 100;

        public float sensorOffset = 45f;
        public float sensorDistance;

        private float lastPathDistance;

        private void Awake()
        {
            lastPathDistance = pathDistance;
            UpdateSporeHead();
            UpdateSporeTrail();
            UpdateSporeSensors();
        }

        // Update is called once per frame
        private void Update()
        {
            lastPathDistance = pathDistance;

            UpdateSporeHead();
            UpdateSporeTrail();
            UpdateSporeSensors();
        }

        public void UpdateSporeHead()
        {
            head.position = pathCreator.path.GetPointAtDistance(pathDistance);
            var direction = pathCreator.path.GetDirectionAtDistance(pathDistance);
            head.rotation = Quaternion.LookRotation(direction);
        }

        public void UpdateSporeTrail()
        {
            var positions = new Vector3[trailVertexCount];

            var currentDistance = pathDistance;

            for (var i = 0; i < trailVertexCount; i++)
                if (currentDistance > 0f)
                {
                    positions[i] = pathCreator.path.GetPointAtDistance(currentDistance);
                    currentDistance -= trailLength / trailVertexCount;
                }
                else
                {
                    positions[i] = pathCreator.path.GetPointAtDistance(0f);
                }

            tail.SetPositions(positions);
        }

        public void UpdateSporeSensors()
        {
            LeftSensorOrigin.localRotation = Quaternion.Euler(0.0f, 0.0f, sensorOffset);
            RightSensorOrigin.localRotation = Quaternion.Euler(0.0f, 0.0f, -sensorOffset);

            var distance = new Vector3(0f, sensorDistance, 0f);

            LeftSensor.localPosition = distance;
            RightSensor.localPosition = distance;
            CenterSensor.localPosition = distance;
        }
    }
}