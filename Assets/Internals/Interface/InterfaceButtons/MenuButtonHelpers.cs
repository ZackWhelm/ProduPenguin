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
        MenuController.Instance.OnWorkSessionStartButtonClick();

    }
}
