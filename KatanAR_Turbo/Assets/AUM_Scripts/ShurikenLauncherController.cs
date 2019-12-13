using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.Common;
using GoogleARCore;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class ShurikenLauncherController : MonoBehaviour
{
    /// <summary>
    /// The first-person camera being used to render the passthrough camera image (i.e. AR
    /// background).
    /// </summary>
    public Camera FirstPersonCamera;

    public GameObject ShurikenPrefab;

    float distanceInstantiate = 0.2f;

    public float launchSpeed = 10f;

    bool touchHasBegun = false;

    Vector2 touchStartPosition;

    Vector2 touchDirection;

    float baseSlideTimer = 2f;

    float slideTimer;

    public GameObject shurikenContainer = default;


    public bool testingInUnity;

    /// <summary>
    /// The rotation in degrees need to apply to prefab when it is placed.
    /// </summary>
    private const float k_PrefabRotation = 180.0f;


    private void OnEnable()
    {
        GameController.ActivateGameObject(GameController.katanaController, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touches.Length == 0)
        {
            return;
        }



        Touch touch = Input.GetTouch(0);

        // Should not handle input if the player is pointing on UI.
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        if (!testingInUnity)
        {
            //ShurikenLaunchWithSlide
            if (touch.phase == TouchPhase.Began && touchHasBegun == false)
            {
                touchHasBegun = true;
                touchStartPosition = touch.position;
                slideTimer = baseSlideTimer;
            }

            if (touchHasBegun)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    touchDirection = touch.position - touchStartPosition;
                    Debug.Log(touchDirection);
                    slideTimer -= Time.deltaTime;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (slideTimer >= 0 && touchDirection.y > 150)
                    {
                        ShurikenLaunch();
                    }

                    touchHasBegun = false;
                }

            }
        }
        else
        {
            if(touch.phase == TouchPhase.Began)
            {
                ShurikenLaunch();
            }
        }
        
        

    }

    private void ShurikenLaunch()
    {
        GameObject shuriken = Instantiate(ShurikenPrefab, FirstPersonCamera.transform.position, FirstPersonCamera.transform.rotation);

        shuriken.transform.SetParent(GameObject.Find("Shurikens").transform, true);

        shuriken.transform.position = FirstPersonCamera.transform.position + FirstPersonCamera.transform.forward * distanceInstantiate;

        shuriken.transform.position = new Vector3(shuriken.transform.position.x, shuriken.transform.position.y - 0.3f, shuriken.transform.position.z);

        shuriken.GetComponent<ShurikenController>().rigibody.velocity = FirstPersonCamera.transform.forward * launchSpeed;

        shuriken.GetComponent<ShurikenController>().rigibody.velocity = new Vector3(
            shuriken.GetComponent<ShurikenController>().rigibody.velocity.x,
            shuriken.GetComponent<ShurikenController>().rigibody.velocity.y + 0.5f,
            shuriken.GetComponent<ShurikenController>().rigibody.velocity.z);
    }


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
}
