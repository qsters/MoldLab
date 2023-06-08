using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenCaptureing : MonoBehaviour
{
    private int index = 0;

    // Update is called once per frame
    public void Capture(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            ScreenCapture.CaptureScreenshot("12_9in_capture_0_" + index + ".png");
            index++;
        }
    }
}