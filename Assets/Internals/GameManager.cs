using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Idle,
    WorkSession,
    StudySession,
    PlaySession,
    Recap,
}

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    public Activity workActivity;
    public Activity studyActivity;
    public Activity playActivity;

    // TODO(zack) should these realy also be activities? im sort of hijacking the renderer for these atm
    public Activity idleActivity;
    public Activity recapActivity;

    public SessionDataController sessionDataController;

    private Activity nextActivity;
    private GameState _state = GameState.Idle;
    
    public GameState State
    {
        get { return _state; }
        set 
        { 
            _state = value;
            if (MenuController.Instance != null)
            {
                MenuController.Instance.UpdateButtonVisibility();
            }
        }
    }

    public static GameManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            sessionDataController.CreateFreshSessionData();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        State = GameState.Idle;
    }

    public void StartWorkSession() {
        if (State == GameState.Idle || State == GameState.Recap) {
            State = GameState.WorkSession;
            sessionDataController.StartSession();
            ActivityRunner.Instance.StartActivity(workActivity);
            nextActivity = workActivity.GetFollowUpActivity();
        }
    }

    public void StartStudySession() {
        if (State == GameState.Idle || State == GameState.Recap) {
            State = GameState.StudySession;
            sessionDataController.StartSession();
            ActivityRunner.Instance.StartActivity(studyActivity);
            nextActivity = studyActivity.GetFollowUpActivity();
        }
    }

    public void StartPlaySession() {
        if (State == GameState.Idle || State == GameState.Recap) {
            State = GameState.PlaySession;
            sessionDataController.StartSession();
            ActivityRunner.Instance.StartActivity(playActivity);
            nextActivity = playActivity.GetFollowUpActivity();
        }
    }

    public void EndSession() {
        if (State == GameState.WorkSession || State == GameState.StudySession || State == GameState.PlaySession) {
            sessionDataController.EndSession();
            ActivityRunner.Instance.ReplaceCurrentActivity(recapActivity);
            State = GameState.Recap;
        }
    }

    public void EndRecap() {
        if (State == GameState.Recap) {
            ActivityRunner.Instance.ReplaceCurrentActivity(idleActivity);
            State = GameState.Idle;
        }
    }

    public bool IsInSession() {
        return (State == GameState.WorkSession || State == GameState.StudySession || State == GameState.PlaySession);
    }

    public bool IsInActiveActivity() {
        Activity currentActivity = ActivityRunner.Instance.currentActivity;
        return currentActivity != null && currentActivity.IsActive;
    }

    public Activity GetNextActivity() {
        switch (State) {
            case GameState.WorkSession:
            case GameState.PlaySession:
            case GameState.StudySession:
                Activity nextActivityToReturn = nextActivity;
                nextActivity = nextActivityToReturn.GetFollowUpActivity();
                return nextActivityToReturn;
            case GameState.Idle:
            case GameState.Recap:
                return null;
        }
        return null;
    }


    
}
