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
        IsExpanded = isExpanded;
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


        playPauseButton.gameObject.SetActive(isExpanded && isInSession && isInActiveActivity);
        workSessionButton.gameObject.SetActive(isExpanded &&!isInSession);
        workSessionButton.gameObject.SetActive(isExpanded &&!isInSession);
        workSessionButton.gameObject.SetActive(isExpanded &&!isInSession);
    }


    public void OnPlayPauseButtonClick() {
        ActivityRunner.Instance.HandlePauseOrResumeToggle();
    }

    public void OnWorkSessionStartButtonClick() {
        GameManager.Instance.StartWorkSession();
    }
}
