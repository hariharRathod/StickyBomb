using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;


public class FaceBookScript : MonoBehaviour
{
    public static FaceBookScript instance;
   
    
    // Awake function from Unity's MonoBehavior
    void Awake()
    {
        instance = this;

        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    void OnHideUnity(bool s)
    { }

    public void LevelCompleted(string LevelNumber)
    {
        var tutParams = new Dictionary<string, object>();
        tutParams["LevelName"] = LevelNumber;
       

        FB.LogAppEvent(
            "LevelCompleted",
            parameters: tutParams
        );
    }

    public void LevelFailed(string LevelNumber)
    {
        var tutParams = new Dictionary<string, object>();
        tutParams["LevelName"] = LevelNumber;


        FB.LogAppEvent(
            "LevelFailed",
            parameters: tutParams
        );
    }

    public void LevelStarted(string LevelNumber)
    {
        var tutParams = new Dictionary<string, object>();
        tutParams["LevelName"] = LevelNumber;


        FB.LogAppEvent(
            "LevelStarted",
            parameters: tutParams
        );
    }
}
