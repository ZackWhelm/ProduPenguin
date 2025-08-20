using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StandardActivity : Activity
{
    protected bool isFinished = false;

    protected virtual void Start()
    {
        // Subclasses should call base.Start() and set their Genre
    }

    public override void StartActivity()
    {
        IsActive = true;
        isFinished = false;
        TimeElapsed = 0f;
        InputCount = 0;
        UpdateStatusText(GetStartStatusText());
    }

    public override void PauseActivity()
    {
        IsPaused = true;
        UpdateStatusText(GetPauseStatusText());
    }

    public override void ResumeActivity()
    {
        IsPaused = false;
        UpdateStatusText(GetResumeStatusText());
    }

    public override void ActivityRoutine()
    {
        if (isFinished)
        {
            UpdateTimerText("Time up");
        }
        else
        {
            UpdateTimerDisplay();
            UpdateStatusText(GetRoutineStatusText());
        }
    }

    public override void ForceEnd() {
        isFinished = true;
        UpdateStatusText(GetEndStatusText());
    }

    public override void HandleActivityEnd()
    {
        isFinished = true;
        if (Genre != ActivityGenre.Idle && Genre != ActivityGenre.Rest) {
            GameManager.Instance.sessionDataController.IncrementActivitiesCompleted();
        }
        else if (Genre == ActivityGenre.Rest) {
            GameManager.Instance.sessionDataController.IncrementRestsCompleted();
        }
        UpdateStatusText(GetEndStatusText());
    }

    protected void UpdateTimerDisplay()
    {
        float timeRemaining = Duration - TimeElapsed;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
        }
        
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        
        string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        UpdateTimerText(timeText);
    }

    protected void UpdateTimerText(string text)
    {
        if (Renderer != null)
        {
            Renderer.UpdateTimerText(text);
        }
    }

    protected void UpdateStatusText(string text)
    {
        if (Renderer != null)
        {
            Renderer.UpdateStatusText(text);
        }
    }

    // Abstract methods that subclasses must implement
    protected abstract string GetStartStatusText();
    protected abstract string GetPauseStatusText();
    protected abstract string GetResumeStatusText();
    protected abstract string GetRoutineStatusText();
    protected abstract string GetEndStatusText();
}
