using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Dependencies")]
    public ButtonView playPauseButton;
    public ButtonView workSessionButton;
    public ButtonView mainMenuButton;

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

    private void UpdateButtonVisibility()
    {
        if (playPauseButton != null)
            playPauseButton.gameObject.SetActive(isExpanded);
        
        if (workSessionButton != null)
            workSessionButton.gameObject.SetActive(isExpanded);
    }


    public void OnPlayPauseButtonClick() {
        ActivityRunner.Instance.HandlePauseOrResumeToggle();
    }

    public void OnWorkSessionStartButtonClick() {
        GameManager.Instance.StartWorkSession();
    }
}
