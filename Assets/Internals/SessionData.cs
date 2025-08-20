using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SessionData
{
    [Header("Session Statistics")]
    public float TimeSpentInSession;
    public int ActivitiesCompleted;
    public int RestsCompleted;
    
    [Header("Session Metadata")]
    public DateTime SessionStartTime;
    public DateTime SessionEndTime;
    
    // Dictionary for dynamic event tracking (for future extensibility)
    [SerializeField]
    private Dictionary<string, int> customEventCounts = new Dictionary<string, int>();
    
    public SessionData()
    {
        ResetSession();
    }
    
    public void StartSession()
    {
        ResetSession();
        SessionStartTime = DateTime.Now;
    }
    
    public void EndSession()
    {
        SessionEndTime = DateTime.Now;
        SaveSessionData();
    }
    
    public void ResetSession()
    {
        TimeSpentInSession = 0f;
        ActivitiesCompleted = 0;
        RestsCompleted = 0;
        customEventCounts.Clear();
    }
    
    public void AddTimeSpent(float timeInSeconds)
    {
        TimeSpentInSession += timeInSeconds;
    }
    
    public void IncrementActivitiesCompleted()
    {
        ActivitiesCompleted++;
    }
    
    public void IncrementRestsCompleted()
    {
        RestsCompleted++;
    }
    
    public void IncrementCustomEvent(string eventName)
    {
        if (customEventCounts.ContainsKey(eventName))
        {
            customEventCounts[eventName]++;
        }
        else
        {
            customEventCounts[eventName] = 1;
        }
    }
    
    public int GetCustomEventCount(string eventName)
    {
        return customEventCounts.ContainsKey(eventName) ? customEventCounts[eventName] : 0;
    }
    
    public Dictionary<string, int> GetAllCustomEvents()
    {
        return new Dictionary<string, int>(customEventCounts);
    }
    
    private void SaveSessionData()
    {
        int sessionIndex = GetNextSessionIndex();
        
        string sessionKey = $"Session_{sessionIndex}";
        string json = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(sessionKey, json);
        
        PlayerPrefs.SetInt("TotalSessions", sessionIndex);
        PlayerPrefs.Save();
        
        Debug.Log($"Session saved with index: {sessionIndex}");
    }
    
    public static SessionData CreateFreshSessionData()
    {
        return new SessionData();
    }
    
    public static SessionData LoadSessionByIndex(int index)
    {
        string sessionKey = $"Session_{index}";
        if (PlayerPrefs.HasKey(sessionKey))
        {
            string json = PlayerPrefs.GetString(sessionKey);
            return JsonUtility.FromJson<SessionData>(json);
        }
        return null;
    }
    
    public static int GetNextSessionIndex()
    {
        return PlayerPrefs.GetInt("TotalSessions", 0) + 1;
    }
    
    public static int GetTotalSessions()
    {
        return PlayerPrefs.GetInt("TotalSessions", 0);
    }
    
    public static List<SessionData> GetAllSessions()
    {
        List<SessionData> sessions = new List<SessionData>();
        int totalSessions = GetTotalSessions();
        
        for (int i = 1; i <= totalSessions; i++)
        {
            SessionData session = LoadSessionByIndex(i);
            if (session != null)
            {
                sessions.Add(session);
            }
        }
        
        return sessions;
    }
    
    public void SaveCurrentSession()
    {
        // This method is now deprecated - sessions are only saved when they end
        Debug.LogWarning("SaveCurrentSession() is deprecated. Sessions are automatically saved when they end.");
    }
    
    public string GetFormattedTimeSpent()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(TimeSpentInSession);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", 
            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }
    
    public float GetSessionDurationInMinutes()
    {
        return TimeSpentInSession / 60f;
    }
    
    public string GetSessionDateString()
    {
        return SessionStartTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
    
    public string GetSessionDurationString()
    {
        TimeSpan duration = SessionEndTime - SessionStartTime;
        return string.Format("{0:D2}:{1:D2}:{2:D2}", 
            duration.Hours, duration.Minutes, duration.Seconds);
    }
    
    public static void ClearAllSessions()
    {
        int totalSessions = GetTotalSessions();
        for (int i = 1; i <= totalSessions; i++)
        {
            string sessionKey = $"Session_{i}";
            PlayerPrefs.DeleteKey(sessionKey);
        }
        PlayerPrefs.DeleteKey("TotalSessions");
        PlayerPrefs.Save();
        Debug.Log("All sessions cleared");
    }
}
