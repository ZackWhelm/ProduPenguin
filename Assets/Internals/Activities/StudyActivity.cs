using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StudyActivity : StandardActivity
{
    [Header("Dependencies")]
    public Activity breakActivity;

    protected override void Start()
    {
        base.Start();
        Genre = ActivityGenre.Playing;
        ShouldCountInputs = false;
    }

    protected override string GetStartStatusText()
    {
        return "Studying in progress...";
    }

    protected override string GetPauseStatusText()
    {
        return "Studying stopped";
    }

    protected override string GetResumeStatusText()
    {
        return "Studying in progress...";
    }

    protected override string GetRoutineStatusText()
    {
        return "Studying";
    }

    protected override string GetEndStatusText()
    {
        return "Studying block finished!";
    }

    public override Activity GetFollowUpActivity() {
        return breakActivity;
    }
}
