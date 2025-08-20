using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SessionDataController : MonoBehaviour
{
    public static SessionData CurrentSessionData;

    public void CreateFreshSessionData() {
        CurrentSessionData = new SessionData();
    }

    public void EndSession()
    {
        CurrentSessionData.EndSession();
    }

    public void ResetSession()
    {
        CurrentSessionData.ResetSession();
    }

    public void StartSession()
    {
        CurrentSessionData.StartSession();
    }

    public SessionData GetCurrData() {
        return CurrentSessionData;
    }

    public void IncrementActivitiesCompleted() {
        CurrentSessionData.IncrementActivitiesCompleted();
    }

    public void IncrementRestsCompleted() {
        CurrentSessionData.IncrementRestsCompleted();
    }
}
