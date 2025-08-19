using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindowController : MonoBehaviour
{
    [DllImport("user32.dll")] private static extern IntPtr GetActiveWindow();
    [DllImport("user32.dll")] private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll")] private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    [DllImport("Dwmapi.dll")] private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    [DllImport("user32.dll")] private static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern short GetAsyncKeyState(int vKey);
    [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();

    private struct MARGINS
    {
        public int cxLeftWidth, cxRightWidth, cyTopHeight, cyBottomHeight;
    }

    const int GWL_EXSTYLE = -20;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;
    private int count = 0;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    private bool wasMouseOverView = false;
    private bool wasMouseDown = false;
    private IntPtr unityWindowHandle = IntPtr.Zero;

    private void Start()
    {
        unityWindowHandle = FindWindow("UnityWndClass", Application.productName);
        if (unityWindowHandle == IntPtr.Zero)
        {
            unityWindowHandle = GetActiveWindow();
        }

        SetWindowPos(unityWindowHandle, HWND_TOPMOST, 0, 0, 0, 0, 0);

        SetWindowLong(unityWindowHandle, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(unityWindowHandle, ref margins);
    }

    private void Update()
    {
        SetWindowPos(unityWindowHandle, HWND_TOPMOST, 0, 0, 0, 0, 0);

        bool isMouseOverView = false;
        if (Interactor.Instance != null) {
            isMouseOverView = Interactor.Instance.IsMouseOverAView();
        }

        bool isMouseDown = (GetAsyncKeyState(0x01) & 0x8000) != 0;
        IntPtr currentForegroundWindow = GetForegroundWindow();
        bool isOurWindowForeground = currentForegroundWindow == unityWindowHandle;

        if (isMouseDown && !wasMouseDown && isMouseOverView) {
            SetWindowLong(unityWindowHandle, GWL_EXSTYLE, WS_EX_LAYERED);
            SetForegroundWindow(unityWindowHandle);
        } else {
            SetClickthrough(!isMouseOverView);
        }

        wasMouseOverView = isMouseOverView;
        wasMouseDown = isMouseDown;
    }

    public void SetClickthrough(bool clickthrough)
    {
        if (clickthrough) {
            SetWindowLong(unityWindowHandle, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        } else {
            SetWindowLong(unityWindowHandle, GWL_EXSTYLE, WS_EX_LAYERED);
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            SetForegroundWindow(unityWindowHandle);
        }
    }
}