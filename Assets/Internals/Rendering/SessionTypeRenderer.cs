using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SessionTypeRenderer : MonoBehaviour
{
    [Header("Session Type Images")]
    public Image SessionTypeImageRef;
    public GameObject SessionTypeObject;

    [Header("Session Type Sprites")]
    public Sprite workSession;
    public Sprite studySession;
    public Sprite playSession;
    public Sprite idleSession;
    public Sprite recapSession;

    public void SetSessionGenreType(ActivityGenre state) {
        UpdateActivitiyGenreIcon(state);
    }

    private void UpdateActivitiyGenreIcon(ActivityGenre state)
    {
        if (SessionTypeImageRef == null) {
            SessionTypeObject.SetActive(false);
            return;
        }

        SessionTypeObject.SetActive(true);
        switch (state)
        {
            case ActivityGenre.Working:
                SessionTypeImageRef.sprite = workSession;
                break;
            case ActivityGenre.Studying:
                SessionTypeImageRef.sprite = studySession;
                break;
            case ActivityGenre.Playing:
                DevLogger.Instance.Log("Setting play session type");
                SessionTypeImageRef.sprite = playSession;
                break;
            case ActivityGenre.Rest:
                SessionTypeImageRef.sprite = idleSession;
                break;
            case ActivityGenre.Idle:
                SessionTypeImageRef.sprite = idleSession;
                break;
            case ActivityGenre.Recap:
                SessionTypeImageRef.sprite = recapSession;
                break;
        }
    }
}
