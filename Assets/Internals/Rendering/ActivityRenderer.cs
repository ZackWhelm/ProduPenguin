using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActivityRenderer : MonoBehaviour
{
    [Header("Dependencies")]
    public SessionTypeRenderer sessionTypeRenderer;
    public ActivityTimerRenderer activityTimerRenderer;
    public PenguinActionRenderer penguinRenderer;
    
    public void SetActivity(Activity activity)
    {
        UpdateActivityDisplay(activity);
    }
    
    
    public void UpdateActivityGenreType(ActivityGenre activityGenre)
    {
        if (sessionTypeRenderer != null)
        {
            sessionTypeRenderer.SetSessionGenreType(activityGenre);
        }
        if (penguinRenderer != null)
        {
            penguinRenderer.SetStateFromGenre(activityGenre);
        }
    }
    
    public void UpdateTimer(string timerText)
    {
        if (activityTimerRenderer != null)
        {
            activityTimerRenderer.UpdateTimer(timerText);
        }
    }

    public void ToggleTimer(bool showTimer)
    {
        if (activityTimerRenderer != null)
        {
            activityTimerRenderer.ToggleTimer(showTimer);
        }
    }
    
    public void UpdateStatus(string statusText)
    {
        // TODO(zack) - implement some ui for status text
        return;
    }
    
    public void SetSoundPlaying(bool isPlaying)
    {
        if (sessionTypeRenderer != null)
        {

        }
    }
    
    private void UpdateActivityDisplay(Activity activity)
    {

    }
    
    public SessionTypeRenderer GetSessionTypeRenderer()
    {
        return sessionTypeRenderer;
    }
}
