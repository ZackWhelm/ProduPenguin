using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayActivity : StandardActivity
{
    [Header("Dependencies")]
    public Activity breakActivity;

    protected override void Start()
    {
        base.Start();
        Genre = ActivityGenre.Playing;
        ShouldCountInputs = true;
    }

    protected override string GetStartStatusText()
    {
        return "Playing in progress...";
    }

    protected override string GetPauseStatusText()
    {
        return "Playing stopped";
    }

    protected override string GetResumeStatusText()
    {
        return "Playing in progress...";
    }

    protected override string GetRoutineStatusText()
    {
        return $"Playing: {InputCount}";
    }

    protected override string GetEndStatusText()
    {
        return "Playing completed!";
    }

    public override Activity GetFollowUpActivity() {
        return breakActivity;
    }
}
