using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleActivity : StandardActivity
{
    [Header("Dependencies")]
    public Activity breakActivity;

    protected override void Start()
    {
        base.Start();
        DurationType = ActivityDurationType.Free;
        Genre = ActivityGenre.Idle;
    }

    protected override string GetStartStatusText()
    {
        return "";
    }

    protected override string GetPauseStatusText()
    {
        return "";
    }

    protected override string GetResumeStatusText()
    {
        return "";
    }

    protected override string GetRoutineStatusText()
    {
        return "";
    }

    protected override string GetEndStatusText()
    {
        return "";
    }

    public override Activity GetFollowUpActivity() {
        return null;
    }
}
