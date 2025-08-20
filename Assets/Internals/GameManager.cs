using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Idle,
    WorkSession,
    StudySession,
    EndedPlaySession,
}

public class GameManager : MonoBehaviour
{
    // This is very hardcoded for now for a basic pomodoro timer
    [Header("Dependencies")]
    public Activity restActivity;
    public Activity workActivity;

    private Activity nextActivity;
    private GameState state = GameState.Idle;

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
    }

    public void StartWorkSession() {
        if (state == GameState.Idle) {
            state = GameState.WorkSession;
            ActivityRunner.Instance.StartActivity(workActivity);
            nextActivity = restActivity;
        }
    }

    public bool IsInWorkSession() {
        return state == GameState.WorkSession;
    }

    public Activity GetNextActivity() {
        Activity result = nextActivity;
        if (nextActivity == restActivity) {
            nextActivity = workActivity;
        }
        else {
            nextActivity = restActivity;
        }
        return result;
    }
}
