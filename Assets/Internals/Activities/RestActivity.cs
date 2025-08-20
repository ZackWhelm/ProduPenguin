using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO(zack): I think activities should be serializable maybe?
public class RestActivity : Activity
{
    [Header("Dependencies")]
    public Activity linkedActivity;

    void Start()
    {
        Genre = ActivityGenre.Rest;
    }


    public override void StartActivity() {
        IsActive = true;
        TimeElapsed = 0f;
        if (Renderer != null)
        {
            Renderer.UpdateStatusText("Break Now");
        }
    }

    public override void PauseActivity() {
        IsActive = false;
        if (Renderer != null)
        {
            Renderer.UpdateStatusText("Break paused");
        }
    }

    public override void ResumeActivity() {
        IsActive = true;
        if (Renderer != null)
        {
            Renderer.UpdateStatusText("Break Now");
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
            Renderer.UpdateStatusText($"Break ending");
        }
    }

    public override Activity GetFollowUpActivity() {
       return linkedActivity;
    }
}
