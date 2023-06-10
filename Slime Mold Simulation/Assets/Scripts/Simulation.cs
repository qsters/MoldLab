using Helpers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class Simulation : MonoBehaviour
{
    // Kernel indexes in textureCS
    public const int updateTrailKernel = 0;
    public const int fadeTextureKernel = 1;
    public const int clearTextureKernel = 2;
    public const int invertTextureKernel = 3;

    // Kernel indexes in sporesCS
    public const int updateSporePosKernel = 0;
    public const int randomizeSporesKernel = 1;

    // Singleton for universal access
    public static Simulation singleton;

    // Textures
    public static RenderTexture trailTexture;

    // Data
    public static Spore[] spores;
    public static SimulationDataPasser passingSimulationData;
    public static SimulationDataScriptableObject simulationData;

    // Buffers
    public static ComputeBuffer sporeBuffer;
    public static ComputeBuffer simulationDataBuffer;

    public static SimulationState simulationState;

    // Compute Shaders
    [SerializeField] public ComputeShader sporesCS;
    [SerializeField] public ComputeShader textureCS;
    [SerializeField] public SimulationDataScriptableObject simulationDataSO;

    [SerializeField] public bool KeepChanges;

    private void Awake()
    {
        // Setting the singleton
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Debug.LogWarning("MORE THAN ONE SIMULATION SINGLETON!!");
        }

        simulationData = simulationDataSO;
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (!KeepChanges)
        {
            simulationData.ResetDataToDefault();
        }

        // Set State
        simulationState = SimulationState.Playing;

        // Set Simulation Info
        SimulationDataPasser.UpdateData();

        // Create Spores
        spores = SporeHandler.GetRandomSpores(simulationData.MaxSporeCount);

        // Resizes and then sets the textures, must change this whenever making shader changes
        TextureHelper.UpdateAndSetRenderTextures();

        // Creating and setting Initial Buffers
        SimulationDataPasser.CreateAndSetData();
        SporeHandler.CreateAndSetSpores();

        // Clears the textures to black, need to do this or else colors are weird in build mode
        TextureHelper.ClearRenderTextures(new[] { trailTexture });

        UpdateSimulation();
    }

    // Update is called once per frame
    private void Update()
    {
        if (simulationState == SimulationState.Playing)
        {
            // Update spore count
            if (spores.Length != simulationData.sporeCount)
            {
                // SporeHandler.UpdateAndSetSporeCount();
            }

            // Set data
            sporesCS.SetFloat("deltaTime", Time.deltaTime);
            textureCS.SetFloat("deltaTime", Time.deltaTime);
            sporesCS.SetFloat("time", Time.realtimeSinceStartup);


            // Fade the trail Map
            if (simulationData.trailLengthPixels < simulationData.MaxSporeTrailLength)
            {
                ComputeHelper.Dispatch(textureCS, Screen.width, Screen.height, 1, fadeTextureKernel);
            }

            // Updating spores position
            ComputeHelper.Dispatch(sporesCS, spores.Length, kernelIndex: updateSporePosKernel);

            // Drawing Spore Position
            ComputeHelper.Dispatch(textureCS, spores.Length, kernelIndex: updateTrailKernel);
        }
    }

    private void OnDestroy()
    {
        ComputeHelper.Release(sporeBuffer, simulationDataBuffer);
    }

    public static void UpdateSimulation()
    {
        // Update settings, and set the buffers
        SimulationDataPasser.UpdateAndSetData();
    }
}