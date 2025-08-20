using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonHelpers : MonoBehaviour
{
    public void MenuButtonClick()
    {
        MenuController.Instance.OnMenuButtonClick();
    }

    public void PlayOrResumeButtonClick()
    {
        MenuController.Instance.OnPlayPauseButtonClick();
    }

    public void StartWorkSessionButtonClick()
    {
        Debug.Log("Start Work Session Button Clicked");
        MenuController.Instance.OnWorkSessionStartButtonClick();
    }

    public void StartStudySessionButtonClick()
    {
        Debug.Log("Start Study Session Button Clicked");
        MenuController.Instance.OnStudySessionStartButtonClick();
    }

    public void StartPlaySessionButtonClick()
    {
        Debug.Log("Start Play Session Button Clicked");
        MenuController.Instance.OnPlaySessionStartButtonClick();
    }
}
