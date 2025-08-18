using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevSettings : MonoBehaviour
{
    public static DevSettings Instance { get; private set; }
    
    [Header("Dev Mode On Flag")]
    public bool DevMode = false;

    [Header("Dev Flags")]
    [SerializeField] bool highlightViews = false;

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

    public bool HighlightViews {
        get { return highlightViews && DevMode; }
        set { highlightViews = value; }
    }
}
