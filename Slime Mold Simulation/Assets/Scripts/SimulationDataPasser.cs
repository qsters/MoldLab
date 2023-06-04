using Helpers;
using UnityEngine;

public enum SimulationState
{
    Playing,
    Paused
}

public enum ColorSchemes
{
    Rx_Gy_B1 = 0, // Default
    Rminusx_Gy_Bminusyx = 1,
    Rx_G1_By = 2,
    Rxy_Gy_B1 = 3,
    R1_Gx_By = 4,
    Rx_GyOverx_BxOvery = 5,
    Rx_Gy_BminusX = 6,
    Rx_Gxy_BminusX = 7
}

public enum ColorSchemeMode
{
    Preset = 0,
    Choice1 = 1,
    Choice2 = 2,
    Gradient = 3
}

public struct SimulationDataPasser
{
    public int sporeCount;
    public float sporeSpeedPPS;
    public float turnSpeed;
    public float sensorDistance;
    public float sensorOffset;
    public float randomnessScale;
    public float trailLengthPixels;
    public int colorSchemeIndex;
    public int colorModeIndex;
    public Vector4 colorChoice1;
    public Vector4 colorChoice2;
    public int screenWidth;
    public int screenHeight;

    // Updates Simulation Settings
    public static void UpdateData()
    {
        var settings = new SimulationDataPasser
        {
            sporeCount = Simulation.simulationData.sporeCount, sporeSpeedPPS = Simulation.simulationData.sporeSpeedPPS,
            turnSpeed = Simulation.simulationData.sporeTurnSpeedRPS,
            sensorDistance = Simulation.simulationData.sporeSensorDistance,
            // Changes angle based on towards or away from angle
            sensorOffset = Simulation.simulationData.sporeSensorOffset * Mathf.Deg2Rad *
                           (Simulation.simulationData.attractedToSpores
                               ? 1
                               : -1),
            randomnessScale = Simulation.simulationData.randomnessScale,
            trailLengthPixels = Simulation.simulationData.trailLengthPixels,
            colorSchemeIndex = (int)Simulation.simulationData.colorScheme,
            colorModeIndex = (int)Simulation.simulationData.colorMode,
            colorChoice1 = Simulation.simulationData.colorChoice1,
            colorChoice2 = Simulation.simulationData.colorChoice2,
            screenWidth = Screen.width, screenHeight = Screen.height
        };
        Simulation.passingSimulationData = settings;
    }

    public static void CreateAndSetData()
    {
        ComputeHelper.CreateStructuredBuffer(ref Simulation.simulationDataBuffer,
            new[] { Simulation.passingSimulationData });

        Simulation.singleton.textureCS.SetBuffer(Simulation.updateTrailKernel, "settings",
            Simulation.simulationDataBuffer);
        Simulation.singleton.textureCS.SetBuffer(Simulation.fadeTextureKernel, "settings",
            Simulation.simulationDataBuffer);

        Simulation.singleton.textureCS.SetBuffer(Simulation.invertTextureKernel, "settings",
            Simulation.simulationDataBuffer);


        Simulation.singleton.sporesCS.SetBuffer(Simulation.updateSporePosKernel, "simulationSettings",
            Simulation.simulationDataBuffer);
    }

    public static void UpdateAndSetData()
    {
        UpdateData();
        CreateAndSetData();
    }
}