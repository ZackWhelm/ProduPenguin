using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ActivityRunner : MonoBehaviour
{
    public static ActivityRunner Instance;

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

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

    private bool[] previousKeyStates = new bool[256];
    private bool[] previousMouseButtonStates = new bool[3];
    private POINT previousMousePosition;
    private float lastInputCheckTime = 0f;
    private const float INPUT_CHECK_INTERVAL = 0.1f;

    void Start()
    {
        GetCursorPos(out previousMousePosition);
    }

    void Update() {
        if (currentActivity == null) {
            return;
        }

        if (currentActivity.IsActive) {
            UpdateActivityTime(currentActivity);
            
            if (Time.time - lastInputCheckTime >= INPUT_CHECK_INTERVAL)
            {
                CheckInputsForActivity(currentActivity);
                lastInputCheckTime = Time.time;
            }
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

    private void CheckInputsForActivity(Activity activity) { 
        if (activity.ShouldCountInputs) {
            CheckKeyboardInputs(activity);
        }
    }

    private void CheckKeyboardInputs(Activity activity)
    {
        for (int i = 0; i < 256; i++)
        {
            short keyState = GetAsyncKeyState(i);
            bool currentKeyState = (keyState & 0x8000) != 0;
            
            if (currentKeyState && !previousKeyStates[i])
            {
                activity.InputCount++;
            }
            previousKeyStates[i] = currentKeyState;
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
