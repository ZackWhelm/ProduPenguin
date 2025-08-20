using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActivityRenderer : MonoBehaviour
{
    public TMP_Text TimerRef;
    public TMP_Text StatusRef;
    public Image SoundPlayingImage;

    public void UpdateTimerText(string text) {
        TimerRef.text = text;
    }

    public void UpdateStatusText(string text) {
        StatusRef.text = text;
    }

    public void SetSoundPlaying(bool isPlaying) {
        SoundPlayingImage.gameObject.SetActive(isPlaying);
    }
}
