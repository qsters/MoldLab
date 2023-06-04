using System.Linq;
using Helpers;
using UnityEngine;

public struct Spore
{
    public Spore(Vector2 position, float angle)
    {
        this.position = position;
        this.angle = angle;
    }

    public Vector2 position;
    public float angle;

    public static Spore[] GetRandomSpores(int spawnAmount)
    {
        var spores = new Spore[spawnAmount];
        for (var i = 0; i < spawnAmount; i++)
        {
            var sporePosition = new Vector2(Random.Range(0, Simulation.simulationData.screenWidth),
                Random.Range(0, Simulation.simulationData.screenHeight));
            var sporeAngle = Random.Range(0, 2 * Mathf.PI);
            var spore = new Spore(sporePosition, sporeAngle);
            spores[i] = spore;
        }

        return spores;
    }

    public static void UpdateAndSetSporeCount()
    {
        GetSporeData();

        var currentSporeCount = Simulation.spores.Length;
        var wantedSporeCount = Simulation.simulationData.sporeCount;

        if (currentSporeCount < wantedSporeCount)
        {
            var missingSporeCount = wantedSporeCount - currentSporeCount;

            var newSpores = new Spore[wantedSporeCount];
            var createdSpores = GetRandomSpores(missingSporeCount);
            Simulation.spores.CopyTo(newSpores, 0);
            createdSpores.CopyTo(newSpores, currentSporeCount);

            Simulation.spores = newSpores;
        }
        else
        {
            var excessSporeCount = currentSporeCount - wantedSporeCount;
            var newSpores = Simulation.spores.SkipLast(excessSporeCount).ToArray();

            Simulation.spores = newSpores;
        }

        CreateAndSetSpores();
    }

    public static void CreateAndSetSpores()
    {
        ComputeHelper.CreateStructuredBuffer(ref Simulation.sporeBuffer, Simulation.spores);

        Simulation.singleton.sporesCS.SetBuffer(Simulation.updateSporePosKernel, "spores", Simulation.sporeBuffer);
        Simulation.singleton.textureCS.SetBuffer(Simulation.updateTrailKernel, "spores", Simulation.sporeBuffer);
    }

    public static void RescaleAndSetSpores(int originalWidth, int originalHeight, int newWidth, int newHeight)
    {
        GetSporeData();
        for (var i = 0; i < Simulation.spores.Length; i++)
        {
            var spore = Simulation.spores[i];

            spore.position.x = spore.position.x / originalWidth * newWidth;
            spore.position.y = spore.position.y / originalHeight * newHeight;

            Simulation.spores[i] = spore;
        }

        CreateAndSetSpores();
    }

    public static void GetSporeData()
    {
        Simulation.sporeBuffer.GetData(Simulation.spores);
    }
}