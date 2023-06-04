using Helpers;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputHandler : MonoBehaviour
{
    public static InputHandler singleton;
    public static bool isPaused;

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
        if (!context.performed || Touch.activeFingers.Count != 2)
        {
            return;
        }

        if (Time.realtimeSinceStartup - Touch.activeFingers[0].currentTouch.startTime > 0.5f)
        {
            return;
        }

        Simulation.simulationState = Simulation.simulationState == SimulationState.Playing
            ? SimulationState.Paused
            : SimulationState.Playing;
    }

    public void OnClearButton(InputAction.CallbackContext context)
    {
        if (!context.canceled || Touch.activeFingers.Count != 3)
        {
            return;
        }

        if (Time.realtimeSinceStartup - Touch.activeFingers[0].currentTouch.startTime > 0.5f)
        {
            return;
        }

        TextureHelper.ClearRenderTextures(new[] { Simulation.trailTexture });
        Simulation.spores = Spore.GetRandomSpores(Simulation.simulationData.sporeCount);
        Spore.CreateAndSetSpores();
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