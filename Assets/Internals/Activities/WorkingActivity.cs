using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorkingActivity : Activity
{
    [Header("Dependencies")]
    public Activity breakActivity;
    private bool isFinishedMining = false;

    void Start()
    {
        Genre = ActivityGenre.Working;
        ShouldCountInputs = true;
    }

    public override void StartActivity() {
        IsActive = true;
        isFinishedMining = false;
        TimeElapsed = 0f;
        InputCount = 0;
        if (Renderer != null)
        {
            Renderer.UpdateStatusText("Mining in progress...");
        }
    }

    public override void PauseActivity() {
        IsPaused = true;
        if (Renderer != null)
        {
            Renderer.UpdateStatusText("Mining stopped");
        }
    }

    public override void ResumeActivity() {
        IsPaused = false;       
        if (Renderer != null)
        {
            Renderer.UpdateStatusText("Mining in progress...");
        }
    }


    public override void ActivityRoutine() {
        if (isFinishedMining) {
            if (Renderer != null)
            {
                Renderer.UpdateTimerText("Time up");
            }
        }
        else {
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
                Renderer.UpdateStatusText($"Mining: {InputCount}");
            }
        }
    }

    public override void HandleActivityEnd() {
        isFinishedMining = true;
        if (Renderer != null)
        {
            Renderer.UpdateStatusText($"Mining completed!");
        }
    }

    public override Activity GetFollowUpActivity() {
        return breakActivity;
    }
}
