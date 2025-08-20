using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivityDurationType 
{
    Free,
    Fixed
}


public enum ActivityGenre
{
    Working, // Keystrokes improve yield of activity
    Studying, // A focus activity like reading or watching educational content
    Playing, // A play activity, playing games or watching movies 
    Rest, // A rest activity, these are breaks from other activities
    Idle, // An idle activity, this plays when an a normal activity is not running.
}


public abstract class Activity : MonoBehaviour
{
    protected ActivityGenre Genre;
    public ActivityDurationType DurationType;
    public ActivityRenderer Renderer;
    public bool ShouldCountInputs;
    public bool IsActive;
    public bool IsPaused = false;
    public float Duration;
    public float TimeElapsed;
    public int InputCount;


    public abstract void StartActivity();
    public abstract void HandleActivityEnd(); // Natural Activity End
    public abstract void ForceEnd(); // Session Ends

    public abstract void PauseActivity();
    public abstract void ResumeActivity();
    public abstract Activity GetFollowUpActivity();

    public abstract void ActivityRoutine();

}
