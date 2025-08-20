using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivityDurationType 
{
    Free,
    Fixed
}


public enum ActivityType 
{
    Mining,
    Studying,
    Rest,
}


public abstract class Activity : MonoBehaviour
{
    public ActivityType Type;
    public ActivityDurationType DurationType;
    public ActivityRenderer Renderer;
    public bool ShouldCountInputs;
    public bool IsActive;
    public float Duration;
    public float TimeElapsed;
    public int InputCount;


    public abstract void StartActivity();
    public abstract void HandleActivityEnd();
    
    public abstract void PauseActivity();
    public abstract void ResumeActivity();

    public abstract void ActivityRoutine();

}
