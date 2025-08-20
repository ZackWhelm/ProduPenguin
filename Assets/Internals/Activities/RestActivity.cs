using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO(zack): I think activities should be serializable maybe?
public class RestActivity : StandardActivity
{
    [Header("Dependencies")]
    public Activity linkedActivity;

    protected override void Start()
    {
        base.Start();
        Genre = ActivityGenre.Rest;
    }

    protected override string GetStartStatusText()
    {
        return "Break Now";
    }

    protected override string GetPauseStatusText()
    {
        return "Break paused";
    }

    protected override string GetResumeStatusText()
    {
        return "Break Now";
    }

    protected override string GetRoutineStatusText()
    {
        return "Break";
    }

    protected override string GetEndStatusText()
    {
        return "Break ending";
    }

    public override Activity GetFollowUpActivity() {
       return linkedActivity;
    }
}
