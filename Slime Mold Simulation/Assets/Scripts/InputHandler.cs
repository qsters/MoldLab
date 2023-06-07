using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputHandler : MonoBehaviour
{
    public static InputHandler singleton;
    public bool isPaused;
    public bool disablePause;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
        if (singleton != null)
        {
            Debug.LogWarning("INPUT SINGLETON NOT SINGLE");
        }

        singleton = this;
    }

    public void OnPauseButton(InputAction.CallbackContext context)
    {
        if (!context.performed || disablePause)
        {
            return;
        }

        isPaused = !isPaused;


        Simulation.simulationState = Simulation.simulationState == SimulationState.Playing
            ? SimulationState.Paused
            : SimulationState.Playing;
    }

    public void OnClearButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            disablePause = true;
        }
        else if (context.performed)
        {
            StopAllCoroutines();
            StartCoroutine(EnableAfterABit());

            if (isPaused)
            {
                return;
            }

            TextureHelper.ClearRenderTextures(new[] { Simulation.trailTexture });
            Simulation.spores = Spore.GetRandomSpores(Simulation.simulationData.sporeCount);
            Spore.CreateAndSetSpores();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(EnableAfterABit());
        }
    }

    private IEnumerator EnableAfterABit()
    {
        yield return new WaitForSeconds(0.1f);

        disablePause = false;
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        var amount = -context.ReadValue<Vector2>().y;
        amount /= 500.0f;

        var sensitivityModifier = 1f;
        if (Keyboard.current.shiftKey.isPressed)
        {
            sensitivityModifier = 0.1f;
        }

        FieldController.selectedField.DragDetected(amount * sensitivityModifier);
    }
}