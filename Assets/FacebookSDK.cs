using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class FacebookSDK : MonoBehaviour
{
    public static FacebookSDK handle;

    void Awake()
    {
        
        
        if (handle == null)
        {
            handle = this;
            DontDestroyOnLoad(this);
            
        }
        else
        {
            Destroy(this);
        }
        
        StartFB();
    }

    private void Start()
    {
        //SceneManager.LoadScene(1);
    }

    

    public void StartFB()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            //Handle FB.Init
            FB.Init(() => { FB.ActivateApp(); });
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {
            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                //Handle FB.Init
                FB.Init(() => { FB.ActivateApp(); });
            }
        }
    }

    public void LogLevelStartedEvent(int levelNumber)
    {
        Debug.Log("Level started: " + levelNumber);
        
        var parameters = new Dictionary<string, object>();
        parameters["LevelNumber"] = levelNumber;
        FB.LogAppEvent(
            "Level Started",
            null,
            parameters
        );
    }
    
    /**
 * Include the Facebook namespace via the following code:
 * using Facebook.Unity;
 *
 * For more details, please take a look at:
 * developers.facebook.com/docs/unity/reference/current/FB.LogAppEvent
 */
    public void LogLevelCompletedEvent (int levelNumber) {
        
        Debug.Log("Level completed: " + levelNumber);
        
        var parameters = new Dictionary<string, object>();
        parameters["LevelNumber"] = levelNumber;
        FB.LogAppEvent(
            "Level Completed",
            null,
            parameters
        );
    }
    
    public void LogLevelFailedEvent (int levelNumber) {
        
        Debug.Log("Level failed: " + levelNumber);
        
        var parameters = new Dictionary<string, object>();
        parameters["LevelNumber"] = levelNumber;
        FB.LogAppEvent(
            "Level Failed",
            null,
            parameters
        );
    }
}
