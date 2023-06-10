using System.Collections;
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
    [SerializeField] private CanvasGroup pauseGroup;

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
        ChangePauseState();
        
        StopAllCoroutines();
        if (isPaused)
        {
            StartCoroutine(UIHelper.FadeCanvasGroup(pauseGroup, 1f, 0.2f));
            
        }
        else
        {
            
            StartCoroutine(UIHelper.FadeCanvasGroup(pauseGroup, 0f, 0.2f));
        }
    }

    public void ChangePauseState()
    {
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

            Simulation.singleton.sporesCS.SetInt("screenHeight", Screen.height);
            Simulation.singleton.sporesCS.SetInt("screenWidth", Screen.width);
            Simulation.singleton.sporesCS.SetInt("maxSpores", Simulation.simulationData.MaxSporeCount);

            ComputeHelper.Dispatch(Simulation.singleton.sporesCS, Simulation.simulationData.MaxSporeCount,
                kernelIndex: Simulation.randomizeSporesKernel);
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