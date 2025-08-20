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
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        State = GameState.Idle;
    }

    public void StartWorkSession() {
        if (State == GameState.Idle) {
            State = GameState.WorkSession;
            ActivityRunner.Instance.StartActivity(workActivity);
            nextActivity = workActivity.GetFollowUpActivity();
        }
    }

    public void StartStudySession() {
        if (State == GameState.Idle) {
            State = GameState.StudySession;
            ActivityRunner.Instance.StartActivity(studyActivity);
            nextActivity = studyActivity.GetFollowUpActivity();
        }
    }

    public void StartPlaySession() {
        if (State == GameState.Idle) {
            State = GameState.PlaySession;
            ActivityRunner.Instance.StartActivity(playActivity);
            nextActivity = playActivity.GetFollowUpActivity();
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
