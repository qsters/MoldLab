using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SimulationDataScriptableObject", menuName = "ScriptableObjects/SimulationData")]
    public class SimulationDataScriptableObject : ScriptableObject
    {
        [Header("Simulation Settings")] [Range(0, 1_000_000)]
        public int sporeCount = 500_000;

        // PPS -> Pixels Per Second
        [Range(0.0f, 500.0f)] public float sporeSpeedPPS;

        // RPS -> Radians Per Second
        [Range(0.0f, 20.0f)] public float sporeTurnSpeedRPS;
        [Range(1.0f, 200.0f)] public float sporeSensorDistance;
        [Range(1.0f, 179.0f)] public float sporeSensorOffset;
        [Range(0.0f, 1.0f)] public float randomnessScale;
        [Range(3.0f, 500.0f)] public float trailLengthPixels;

        public bool attractedToSpores = true;
        public ColorSchemes colorScheme = ColorSchemes.Rx_Gy_B1;
        public ColorSchemeMode colorMode = ColorSchemeMode.Preset;
        public Color colorChoice1 = Color.cyan;
        public Color colorChoice2 = Color.red;

        [HideInInspector] public int screenHeight = Screen.height;
        [HideInInspector] public int screenWidth = Screen.width;

        // Maxes
        public readonly float MaxRandomnessScale = 1.0f;
        public readonly int MaxSporeCount = 1_000_000;
        public readonly float MaxSporeSensorAngle = 170.0f;
        public readonly float MaxSporeSensorDistance = 200.0f;
        public readonly float MaxSporeSpeed = 500.0f;
        public readonly float MaxSporeTrailLength = 500.0f;
        public readonly float MaxSporeTurnSpeed = 12.0f;
        public readonly float MinRandomnessScale = 0.0f;

        // Mins
        public readonly int MinSporeCount = 1;
        public readonly float MinSporeSensorAngle = 10.0f;
        public readonly float MinSporeSensorDistance = 3.0f;
        public readonly float MinSporeSpeed = 10.0f;
        public readonly float MinSporeTrailLength = 3.0f;
        public readonly float MinSporeTurnSpeed = 0.0f;
        
        public void ResetDataToDefault()
        {
            sporeCount = 500_000;

            sporeSpeedPPS = 65.0f;

            // RPS -> Radians Per Second
            sporeTurnSpeedRPS = 5.0f;
            sporeSensorDistance = 5.0f;
            sporeSensorOffset = 45.0f;
            randomnessScale = 0.0f;
            trailLengthPixels = 100.0f;

            attractedToSpores = true;

            colorScheme = ColorSchemes.Rx_Gy_B1;
            colorMode = ColorSchemeMode.Preset;
            colorChoice1 = Color.cyan;
            colorChoice2 = Color.red;

            screenHeight = Screen.height;
            screenWidth = Screen.width;
        }
    }
}