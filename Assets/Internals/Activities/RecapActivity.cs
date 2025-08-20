using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecapActivity : StandardActivity
{
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

    public void SetRecapData(SessionData data) {
        UpdateTimerText("Blocks completed: " + data.ActivitiesCompleted);
    }

    public override void ActivityRoutine()
    {
        return;
    }
}
