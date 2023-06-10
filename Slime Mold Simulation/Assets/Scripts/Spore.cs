using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public struct Spore
{
    public Spore(Vector2 position, float angle)
    {
        this.position = position;
        this.angle = angle;
    }

    public Vector2 position;
    public float angle;
}

public static class SporeHandler
{
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

    public static void CreateAndSetSpores()
    {
        ComputeHelper.CreateStructuredBuffer(ref Simulation.sporeBuffer, Simulation.spores);

        Simulation.singleton.sporesCS.SetBuffer(Simulation.updateSporePosKernel, "spores", Simulation.sporeBuffer);
        Simulation.singleton.sporesCS.SetBuffer(Simulation.randomizeSporesKernel, "spores", Simulation.sporeBuffer);
        Simulation.singleton.textureCS.SetBuffer(Simulation.updateTrailKernel, "spores", Simulation.sporeBuffer);
    }
}