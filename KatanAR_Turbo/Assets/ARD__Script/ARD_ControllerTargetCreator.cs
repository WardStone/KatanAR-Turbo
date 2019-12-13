namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.EventSystems;

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif

    /// <summary>
    /// Apparration des cibles en appuyant
    /// </summary>
    public class ARD_ControllerTargetCreator : MonoBehaviour
    {
        #region InOriginal Variable (modified)
        public Camera FirstPersonCamera;

        private bool m_IsQuitting = false;
        #endregion

        [Space (10)]
        [Header("Ajout")]

        public int level = 0;
        public GameObject[] GameAreaRoom = default;
        public ShurikenLauncherController shurikenLauncherController = default;
        GameObject shurikenPhase = default;

        [SerializeField]
        bool isRoomCreate = false;

        public void Awake()
        {
            #region InOriginal Awake (not modified)
            // Enable ARCore to target 60fps camera capture frame rate on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
            Application.targetFrameRate = 60;
            #endregion
        }

        private void Start()
        {
            shurikenLauncherController = GetComponent<ShurikenLauncherController>();
            //shurikenPhase = shurikenLauncherController.gameObject;
            //shurikenPhase.SetActive(false);
        }

        public void Update()
        {
            #region InOriginal Update (not modified)

            _UpdateApplicationLifecycle();

            // Est ce que le joueur a touché l'ecran depuis la dernière frame ? Si non, attendre la prochaine frame
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }

            // Ne pas prendre en compte l'input s'il est sur un élément de l'UI
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }

            #endregion

            // Raycast depuis la position du player vers la direction qu'il a touché, en cherchant une surface
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                //creation de la room et génération des cibles
                if (!isRoomCreate)
                {
                    // Use hit pose and camera pose pour vérifier qui ne touche pas l'arrière de la surface (dans ce cas pas d'ancre à spawn)
                    if ((hit.Trackable is DetectedPlane) && Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0)
                    {
                        Debug.Log("Hit at back of the current DetectedPlane");
                    }
                    else
                    {
                        // instancie un prefab
                        GameObject prefab;

                        if (hit.Trackable is DetectedPlane)
                        {
                            DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
                            if (detectedPlane.PlaneType == DetectedPlaneType.HorizontalUpwardFacing)
                            {
                                if (!isRoomCreate)
                                {


                                    prefab = GameAreaRoom[level];

                                    isRoomCreate = true;

                                    

                                }else { prefab = null; }
                            } else { prefab = null; }
                        } else { prefab = null; }

                        // Instancie le prefab
                        var gameObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);

                        // Create an anchor to allow ARCore to track the hitpoint as understanding of
                        // the physical world evolves.
                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                        // Make game object a child of the anchor.
                        gameObject.transform.parent = anchor.transform;
                    }
                }
                //changement de phase
                else
                {
                    shurikenPhase.SetActive(true);
                }
            }
            
            if(isRoomCreate)
            {
                GameController.ActivateGameObject(GameController.observationController, true);
            }
        }



        #region InOriginal Fonctions (not modified)

        private void _UpdateApplicationLifecycle()
        {
            // Quite le jeu quand tu appuies sur echap
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
        
        #endregion
    }
}
