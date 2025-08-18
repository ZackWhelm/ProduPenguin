using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityRunner : MonoBehaviour
{
    public static ActivityRunner Instance;

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

   [Header("Setup")]
    public float delayBetweenActivities = 5f;

    [Header("State")]
    public Activity currentActivity;
    public ActivityTracker activityTracker;

    void Update() {
        if (currentActivity == null) {
            return;
        }

        if (currentActivity.IsActive) {
            UpdateActivityTime(currentActivity);
            activityTracker.TrackActivityInput(currentActivity);
        }

        currentActivity.ActivityRoutine();
    }

    private void UpdateActivityTime(Activity activity) {
        activity.TimeElapsed += Time.deltaTime;
    
        if (activity.DurationType == ActivityDurationType.Fixed) {
            if (activity.TimeElapsed >= activity.Duration) {
                activity.IsActive = false;
                HandleActivityEnd();
            }
        }
    }

    private void HandleActivityEnd() {
        currentActivity.HandleActivityEnd();
        StartCoroutine(StartNextActivityAfterDelay(delayBetweenActivities));
    }

    public void StartActivity(Activity activity) {
        currentActivity = activity;
        currentActivity.StartActivity();
    }

    public void StopActivity() {
        if (currentActivity != null)
        {
            currentActivity.StopActivity();
            currentActivity.IsActive = false;
        }

    } 

    private IEnumerator StartNextActivityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartActivity(GameManager.Instance.GetNextActivity());
    }
}
