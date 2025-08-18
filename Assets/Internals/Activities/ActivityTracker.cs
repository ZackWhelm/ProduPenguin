using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ActivityTracker : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();


    private float lastInputCheckTime = 0f;
    private const float INPUT_CHECK_INTERVAL = 0.04f;
    private bool[] previousMouseButtonStates = new bool[2];

    public void TrackActivityInput(Activity activity)
    {
        if (!activity.ShouldCountInputs)
            return;

        if (Time.time - lastInputCheckTime >= INPUT_CHECK_INTERVAL)
        {
            CheckKeyboardInputs(activity);
            CheckMouseInputs(activity);
        }
    }

    private void CheckKeyboardInputs(Activity activity)
    {
        for (int i = 0; i < 256; i++)
        {
            short keyState = GetAsyncKeyState(i);
            bool wasPressed = (keyState & 0x0001) != 0;
            
            if (wasPressed)
            {
                activity.InputCount++;
            }
        }
    }

    private void CheckMouseInputs(Activity activity)
    {
        int[] mouseButtons = { 0x01, 0x02 };
        
        for (int i = 0; i < 2; i++)
        {
            short mouseState = GetAsyncKeyState(mouseButtons[i]);
            bool currentMouseState = (mouseState & 0x8000) != 0;
            
            if (currentMouseState && !previousMouseButtonStates[i])
            {
                activity.InputCount++;
            }
            previousMouseButtonStates[i] = currentMouseState;
        }
    }
}
