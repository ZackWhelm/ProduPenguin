using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActivityTimerRenderer : MonoBehaviour
{
    [Header("Dependencies")]
    public GameObject TimerBubbleObject;
    public TMP_Text TimerRef;

    public void UpdateTimer(string timerText) {
        if (TimerRef != null) {
            TimerRef.text = timerText;
        }
    }

    public void ToggleTimer(bool showTimer) {
        if (TimerBubbleObject != null) {
            TimerBubbleObject.SetActive(showTimer);
        }
    }
}
