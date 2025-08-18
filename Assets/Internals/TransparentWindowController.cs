using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindowController : MonoBehaviour
{
    [DllImport("user32.dll")] private static extern IntPtr GetActiveWindow();
    [DllImport("user32.dll")] private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    [DllImport("Dwmapi.dll")] private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    private struct MARGINS
    {
        public int cxLeftWidth, cxRightWidth, cyTopHeight, cyBottomHeight;
    }

    const int GWL_EXSTYLE = -20;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    private void Start()
    {
        IntPtr hWnd = GetActiveWindow();

        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);

        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);
    }

    private void Update()
    {
        if (Interactor.Instance != null) {
            SetClickthrough(!Interactor.Instance.IsMouseOverAView());
        }
        else {
            SetClickthrough(true);
        }
    }

    public void SetClickthrough(bool clickthrough)
    {
        if (clickthrough)
            SetWindowLong(GetActiveWindow(), GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        else
            SetWindowLong(GetActiveWindow(), GWL_EXSTYLE, WS_EX_LAYERED);
    }
}
