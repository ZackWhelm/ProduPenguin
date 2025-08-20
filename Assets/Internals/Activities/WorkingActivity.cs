using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorkingActivity : StandardActivity
{
    [Header("Dependencies")]
    public Activity breakActivity;

    protected override void Start()
    {
        base.Start();
        Genre = ActivityGenre.Working;
        ShouldCountInputs = true;
    }

    protected override string GetStartStatusText()
    {
        return "Mining in progress...";
    }

    protected override string GetPauseStatusText()
    {
        return "Mining stopped";
    }

    protected override string GetResumeStatusText()
    {
        return "Mining in progress...";
    }

    protected override string GetRoutineStatusText()
    {
        return $"Mining: {InputCount}";
    }

    protected override string GetEndStatusText()
    {
        return "Mining completed!";
    }

    public override Activity GetFollowUpActivity() {
        return breakActivity;
    }
}
