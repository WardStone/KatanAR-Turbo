using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameObject messageBar;

    private bool m_IsQuitting = false;

    public static bool startPassed = false;

    public static GameObject scanningController;
    public static GameObject menuController;
    public static GameObject observationController;
    public static GameObject combatController;
    public static GameObject shurikenController;
    public static GameObject katanaController;
    public static GameObject resultController;

    public static GameObject observationCanvas;

    public void Awake()
    {
        // Enable ARCore to target 60fps camera capture frame rate on supported devices.
        // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
        Application.targetFrameRate = 60;

        messageBar = GameObject.Find("Message Bar");

        scanningController = GameObject.Find("Scanning_Controller");
        menuController = GameObject.Find("Menu_Controller");
        observationController = GameObject.Find("Observation_Controller");
        combatController = GameObject.Find("Combat_Controller");
        shurikenController = GameObject.Find("Shuriken_Controller");
        katanaController = GameObject.Find("Katana_Controller");
        resultController = GameObject.Find("Result_Controller");

        observationCanvas = GameObject.Find("ObservationCanvas");
    }

    private void Start()
    {
        startPassed = true;
    }

    void Update()
    {
        _UpdateApplicationLifecycle();
    }


    /// <summary>
    /// Check and update the application lifecycle.
    /// </summary>
    private void _UpdateApplicationLifecycle()
    {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (m_IsQuitting)
        {
            return;
        }

        // Quit if ARCore was unable to connect and give Unity some time for the toast to
        // appear.
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            _ShowAndroidToastMessage("Camera permission is needed to run this application.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
        else if (Session.Status.IsError())
        {
            _ShowAndroidToastMessage(
                "ARCore encountered a problem connecting.  Please start the app again.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
    }

    /// <summary>
    /// Actually quit the application.
    /// </summary>
    private void _DoQuit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Show an Android toast message.
    /// </summary>
    /// <param name="message">Message string to show in the toast.</param>
    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject =
                    toastClass.CallStatic<AndroidJavaObject>(
                        "makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }

    public static void ActivateGameObject(GameObject gameObject, bool activation)
    {
        if(gameObject != null)
            if(gameObject.activeSelf != activation)
                gameObject.SetActive(activation);
    }

}
