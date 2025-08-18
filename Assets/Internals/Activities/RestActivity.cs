using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO(zack): I think activities should be serializable maybe?
public class RestActivity : Activity
{
    public override void StartActivity() {
        IsActive = true;
        TimeElapsed = 0f;
        if (Renderer != null)
        {
            Renderer.SetBackgroundColor(Color.blue);
            Renderer.UpdateStatusText("Break Now");
        }
    }

    public override void StopActivity() {
        IsActive = false;
        if (Renderer != null)
        {
            Renderer.UpdateStatusText("Break paused");
        }
    }

    public override void ActivityRoutine() {
        float timeRemaining = Duration - TimeElapsed;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
        }
        
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        if (Renderer != null)
        {
            Renderer.UpdateTimerText(timeText);
            Renderer.UpdateStatusText("Break");
        }
    }

    public override void HandleActivityEnd() {
        if (Renderer != null)
        {
            Renderer.SetBackgroundColor(Color.green);
            Renderer.UpdateStatusText($"Break ending");
        }
    }
}
