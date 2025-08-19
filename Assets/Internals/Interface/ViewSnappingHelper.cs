using System;
using UnityEngine;
using System.Runtime.InteropServices;

public class ViewSnappingHelper : MonoBehaviour
{
    public static ViewSnappingHelper Instance { get; private set; }

    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private float taskbarHeight = 0f;
    private bool taskbarHeightCalculated = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CalculateTaskbarHeight();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CalculateTaskbarHeight()
    {
        try
        {
            IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);
            if (taskbarHandle != IntPtr.Zero)
            {
                RECT taskbarRect;
                if (GetWindowRect(taskbarHandle, out taskbarRect))
                {
                    taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;
                    taskbarHeightCalculated = true;
                }
            }
        }
        catch (System.Exception e)
        {
            DevLogger.Instance.Log("Could not calculate taskbar height: " + e.Message);
            taskbarHeight = 40f;
            taskbarHeightCalculated = true;
        }
    }

    public Vector2 SnapToScreen(Vector2 position, Vector2 viewSize, RectTransform parentRect)
    {
        if (!taskbarHeightCalculated || parentRect == null) return position;

        Vector2 parentSize = parentRect.rect.size;
        float canvasHeight = parentSize.y;
        float canvasWidth = parentSize.x;

        float taskbarTopYPos = -parentSize.y/2.0f + taskbarHeight;
        
        Vector2 targPos = position;
        float viewHeight = viewSize.y;
        float viewWidth = viewSize.x;



        if (targPos.y - viewHeight/2.0f < taskbarTopYPos)
        {
            targPos.y = taskbarTopYPos + viewHeight/2.0f;
        }

        if (targPos.y + viewHeight/2.0f > canvasHeight/2.0f)
        {
            targPos.y = canvasHeight/2.0f - viewHeight/2.0f;
        }

        if (targPos.x - viewWidth/2.0f < -canvasWidth/2.0f)
        {
            targPos.x = -canvasWidth/2.0f + viewWidth/2.0f;
        }

        if (targPos.x + viewWidth/2.0f > canvasWidth/2.0f)
        {
            targPos.x = canvasWidth/2.0f - viewWidth/2.0f;
        }


        return targPos;
    }

    public float GetTaskbarHeight()
    {
        return taskbarHeight;
    }

    public bool IsTaskbarHeightCalculated()
    {
        return taskbarHeightCalculated;
    }
}
