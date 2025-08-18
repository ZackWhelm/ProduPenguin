using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is very hardcoded for now for a basic pomodoro timer
    [Header("Dependencies")]
    public Activity restActivity;
    public Activity workActivity;

    private Activity nextActivity;

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

    void Start()
    {
        ActivityRunner.Instance.StartActivity(workActivity);
        nextActivity = restActivity;
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
