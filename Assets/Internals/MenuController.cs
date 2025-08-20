using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Main Menu Dependencies")]
    public ButtonView workSessionButton;
    public ButtonView studySessionButton;
    public ButtonView playSessionButton;
    public ButtonView mainMenuButton;

    [Header("Session Dependencies")]
    public ButtonView playPauseButton;

    private bool isExpanded = false;

    public static MenuController Instance;

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

    public void OnMenuButtonClick() {
        IsExpanded = !IsExpanded;
    }

    public bool IsExpanded
    {
        get { return isExpanded; }
        set 
        { 
            isExpanded = value;
            UpdateButtonVisibility();
        }
    }

    public void UpdateButtonVisibility()
    {
        bool isInSession = GameManager.Instance.IsInSession();
        bool isInActiveActivity = GameManager.Instance.IsInActiveActivity();


        playPauseButton.gameObject.SetActive(IsExpanded && isInSession && isInActiveActivity);
        workSessionButton.gameObject.SetActive(IsExpanded &&!isInSession);
        studySessionButton.gameObject.SetActive(IsExpanded &&!isInSession);
        playSessionButton.gameObject.SetActive(IsExpanded &&!isInSession);
    }


    public void OnPlayPauseButtonClick() {
        ActivityRunner.Instance.HandlePauseOrResumeToggle();
    }

    public void OnWorkSessionStartButtonClick() {
        DevLogger.Instance.Log("OnWorkSessionStartButtonClick");
        GameManager.Instance.StartWorkSession();
    }

    public void OnStudySessionStartButtonClick() {
        DevLogger.Instance.Log("OnStudySessionStartButtonClick");
        GameManager.Instance.StartStudySession();
    }

    public void OnPlaySessionStartButtonClick() {
        DevLogger.Instance.Log("OnPlaySessionStartButtonClick");
        GameManager.Instance.StartPlaySession();
    }
}
