using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;

public class SDK : MonoBehaviour
{
    private void Awake()
    {
        GameAnalytics.Initialize();
        
        Debug.Log("GA");
        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, "World_01", "Stage_01", "Level_Progress");
    }
}
