using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DevLogger : MonoBehaviour
{
    public static DevLogger Instance { get; private set; }
    
    [Header("Dependencies")]
    public TMP_Text devText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Log(string message) {
        devText.text = message;
    }
}
