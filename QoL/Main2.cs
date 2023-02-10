using System;
using System.Collections;
using MelonLoader;
using MoleMole;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using UnityEngine.UI;

namespace FreeCam
{
    public class Main2 : MonoBehaviour
    {
        public Main2(IntPtr ptr) : base(ptr)
        {
        }

        public Main2() : base(ClassInjector.DerivedConstructorPointer<Main2>())
        {
            ClassInjector.DerivedConstructorBody(this);
        }

        #region Properties

        public static int TargetFPS = 300;

        public static bool ShowCd = true;
        public static bool ShowMenu = true;

        public static bool EnableFastTxt = true;
        public static bool EnableFastCutscene;
        public static bool EnableCam;
        public static bool HideMinimap;
        public static bool DisableLoadingScreen = true;


        public static bool CamIsActive;

        public static bool timerverify;
        public static bool multiply;
        public static bool FPS;
        public static bool GiantCamera;
        public static bool FreeCam = false;
        public static bool CutsceneCam = false;
        

        public static GameObject ActiveAvatar;
        public static GameObject AvatarRoot;

        public static Transform NewcamTarget;
        public static float DistanceFromTarget = 20f;
        public static float XOffset;
        public static float YOffset = 1.5f;
        public static float ZOffset;
        public static float NewcamFOV = 45f;


        public static Camera Maincam;
        public static Camera Newcam;
        public static Camera CamDup;
        public static GameObject MaincamObj;
        public static GameObject NewcamObj;
        public static GameObject CameraDupe;
        public static GameObject CutsceneCamera;
        public static Camera CutCam;

        public static GameObject CharModel;
        public static GameObject CharHead;
        public static GameObject Character;

        private static GameObject _topRight;
        private static GameObject _topLeft;
        private static GameObject _playerProfile;
        private static GameObject _quest;
        private static GameObject _latency;
        private static GameObject _chat;
        private static GameObject _minimap;

        public static GameObject Txt;
        public static GameObject Cutscene;
        public static GameObject UID;
        public static GameObject UID2;


        public static GUILayoutOption[] ButtonSize;
        public static GUILayoutOption[] ButtonSize2;

        float offset = 0f;

        public static string UIDText = "Hello!";

        private static bool _isVisible = true;
        private static float uiScale = 1;

        private static int _winW = 250;
        private static int _winH = 50;
        public Rect windowRect = new Rect((Screen.width - _winW) / 2, 100, _winW, _winH);

        float i = 1;
        public static float interval = 60;
        float growth = 0.1f;
        public static float BaseY = 1.4f;
        public static float BaseCamera = 4.0f;
        public float setsensitivity = 5f;
        float rotationX=0f;
        float rotationY=0f;
        float cameraSpeed = 1f;
        float FOV = 50f;

        Vector3 forwardRelativeVerticalInput;
        Vector3 rightReltativeHorizontalInput;
        Vector3 cameraRelativeMovement;
        Vector3 right;
        Vector3 forward;
        float playerHorizontalInput;
        float playerVerticalInput;

        #endregion

        public void Start()
        {
            MelonCoroutines.Start(FindElements());
        }

        public void OnGUI()
        {
            if (ShowMenu)
                windowRect = GUILayout.Window(2, windowRect, (GUI.WindowFunction) UIDWindow, "FreeCam by Cybertron231",
                    new GUILayoutOption[0]);
        }

        public void UIDWindow(int id)
        {
            if (id == 2)
            {
                ButtonSize = new[]
                {
                    GUILayout.Width(40),
                    GUILayout.Height(20)
                };
                ButtonSize2 = new[]
                {
                    GUILayout.Width(60),
                    GUILayout.Height(20)
                };
                FreeCam = GUILayout.Toggle(FreeCam, "Free Camera", new GUILayoutOption[0]);
                CutsceneCam = GUILayout.Toggle(CutsceneCam, "Cutscene Camera", new GUILayoutOption[0]);
                GUILayout.Label($"Camera Speed: {cameraSpeed:F1}", new GUILayoutOption[0]);
                cameraSpeed = GUILayout.HorizontalSlider(cameraSpeed, 0.1f, 10f, new GUILayoutOption[0]);
                cameraSpeed = float.Parse(GUILayout.TextField(cameraSpeed.ToString(), new GUILayoutOption[0]));
                GUILayout.Label($"Camera Sensitivity: {setsensitivity:F1}", new GUILayoutOption[0]);
                setsensitivity = GUILayout.HorizontalSlider(setsensitivity, 1f, 20f, new GUILayoutOption[0]);
                GUILayout.Label($"Camera FOV: {FOV:F1}", new GUILayoutOption[0]);
                FOV = GUILayout.HorizontalSlider(FOV, 30f, 180f, new GUILayoutOption[0]);




            }

            GUI.DragWindow();
        }

        public void Update()
        {
            if (ShowMenu)
                Focused = false;

            UpdateInput();
            FindObjects();
            FreeCameraMode();

            if (MaincamObj && NewcamObj && Maincam)
            {

            }
                //SetCam();
        }

        private void FreeCameraMode()
        {
                if (FreeCam == true)
                {
                    if (MaincamObj.active == true)
                    {
                        if (CameraDupe == null && FreeCam == true)
                            CameraDupe = Instantiate(MaincamObj);
                        CameraDupe.SetActive(true);
                        MaincamObj.SetActive(false);
                        if (CamDup == null)
                            CamDup = CameraDupe.GetComponent<Camera>();
                    }

                    playerVerticalInput = Input.GetAxis("Vertical");
                    playerHorizontalInput = Input.GetAxis("Horizontal");
                    forward = Camera.main.transform.forward;
                    right = Camera.main.transform.right;

                    forwardRelativeVerticalInput = playerVerticalInput * forward ;
                    rightReltativeHorizontalInput = playerHorizontalInput * right;

                    cameraRelativeMovement = (forwardRelativeVerticalInput + rightReltativeHorizontalInput) * cameraSpeed;
                    CameraDupe.transform.Translate(cameraRelativeMovement, Space.World);
                    rotationX -= Input.GetAxis("Mouse Y") * setsensitivity;
                    rotationY += Input.GetAxis("Mouse X") * setsensitivity;
                    CameraDupe.transform.localEulerAngles=new Vector3(rotationX, rotationY, 0);

                    CamDup.fieldOfView = FOV;
                    
                }
                else
                {
                    if (MaincamObj.active == false)
                    {
                        MaincamObj.SetActive(true);
                        Destroy(CameraDupe);

                    }
                }
            if(CutsceneCam == true)
            {
                if (CutCam == null)
                    CutCam = CutsceneCamera.GetComponent<Camera>();

                float playerVerticalInput = Input.GetAxis("Vertical");
                float playerHorizontalInput = Input.GetAxis("Horizontal");
                Vector3 forward = Camera.main.transform.forward;
                Vector3 right = Camera.main.transform.right;

                Vector3 forwardRelativeVerticalInput = playerVerticalInput * forward;
                Vector3 rightReltativeHorizontalInput = playerHorizontalInput * right;

                Vector3 cameraRelativeMovement = (forwardRelativeVerticalInput + rightReltativeHorizontalInput) * cameraSpeed;
                CutsceneCamera.transform.Translate(cameraRelativeMovement, Space.World);
                rotationX -= Input.GetAxis("Mouse Y") * setsensitivity;
                rotationY += Input.GetAxis("Mouse X") * setsensitivity;
                CutsceneCamera.transform.localEulerAngles = new Vector3(-rotationX+offset, rotationY, -rotationX);

                CutCam.fieldOfView = FOV;
            }
            else
            {
                CutsceneCamera = null;
            }
        }

        private void UpdateInput()
        {
            if (Input.GetKeyDown(KeyCode.I))
                ToggleHUD();
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.I))
                ShowMenu = !ShowMenu;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                DistanceFromTarget--;
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                DistanceFromTarget++;
        }

        #region MainFunctions

        public void ToggleHUD()
        {
            switch (_isVisible)
            {
                case true:
                    if (_topRight)
                        _topRight.transform.localScale = new Vector3(0, 0, 0);
                    if (_topLeft)
                        _topLeft.transform.localScale = new Vector3(0, 0, 0);
                    if (_quest)
                        _quest.transform.localScale = new Vector3(0, 0, 0);
                    if (_playerProfile)
                        _playerProfile.transform.localScale = new Vector3(0, 0, 0);
                    if (_latency)
                        _latency.transform.localScale = new Vector3(0, 0, 0);
                    if (_chat)
                        _chat.transform.localScale = new Vector3(0, 0, 0);
                    if (UID)
                        UID.transform.localScale = new Vector3(0, 0, 0);
                    if (_minimap)
                        _minimap.transform.localScale =
                            HideMinimap ? new Vector3(0, 0, 0) : new Vector3(uiScale, uiScale, uiScale);
                    _isVisible = false;
                    break;
                default:
                    if (_topRight)
                        _topRight.transform.localScale = new Vector3(uiScale, uiScale, uiScale);
                    if (_topLeft)
                        _topLeft.transform.localScale = new Vector3(uiScale, uiScale, uiScale);
                    if (_quest)
                        _quest.transform.localScale = new Vector3(uiScale, uiScale, uiScale);
                    if (_playerProfile)
                        _playerProfile.transform.localScale = new Vector3(uiScale, uiScale, uiScale);
                    if (_latency)
                        _latency.transform.localScale = new Vector3(uiScale, uiScale, uiScale);
                    if (_chat)
                        _chat.transform.localScale = new Vector3(uiScale, uiScale, uiScale);
                    if (UID)
                        UID.transform.localScale = new Vector3(uiScale, uiScale, uiScale);
                    if (_minimap)
                        _minimap.transform.localScale = new Vector3(uiScale, uiScale, uiScale);
                    _isVisible = true;
                    break;
            }
        }

        #endregion

        #region HelperFunctions

        private static IEnumerator FindElements()
        {
            for (;;)
            {
                if (EnableFastTxt && Txt == null)
                    Txt = GameObject.Find(
                        "/Canvas/Dialogs/DialogLayer(Clone)/TalkDialog/GrpTalk/GrpConversation/TalkGrpConversation_1(Clone)/Content/TxtDesc");
                if (EnableFastCutscene && Cutscene == null)
                    Cutscene = GameObject.Find("/Canvas/Pages/InLevelCutScenePage");
                if (UID == null)
                    UID = GameObject.Find("/BetaWatermarkCanvas(Clone)/Panel/TxtUID");
                if (UID2 == null)
                    UID2 = GameObject.Find(
                        "/Canvas/Pages/PlayerProfilePage/GrpProfile/Right/GrpPlayerCard/UID/Layout/PlayerID");
                yield return new WaitForSeconds(1f);
            }
        }

        private void FindObjects()
        {
            if (MaincamObj == null)
                MaincamObj = GameObject.Find("/EntityRoot/MainCamera(Clone)");
            if (AvatarRoot == null)
                AvatarRoot = GameObject.Find("/EntityRoot/AvatarRoot");
            if (_topRight == null)
                _topRight = GameObject.Find("/Canvas/Pages/InLevelMainPage/GrpMainPage/GrpMainBtn/GrpMainToggle");
            if (_topLeft == null)
                _topLeft = GameObject.Find("/Canvas/Pages/InLevelMainPage/GrpMainPage/MapInfo/GrpButtons");
            if (_quest == null)
                _quest = GameObject.Find("/Canvas/Pages/InLevelMainPage/GrpMainPage/MapInfo/BtnToggleQuest");
            if (_playerProfile == null)
                _playerProfile = GameObject.Find("/Canvas/Pages/InLevelMainPage/GrpMainPage/MapInfo/BtnPlayerProfile");
            if (_latency == null)
                _latency = GameObject.Find("/Canvas/Pages/InLevelMainPage/GrpMainPage/NetworkLatency");
            if (_chat == null)
                _chat = GameObject.Find("/Canvas/Pages/InLevelMainPage/GrpMainPage/Chat/Content");
            if (_minimap == null)
                _minimap = GameObject.Find("/Canvas/Pages/InLevelMainPage/GrpMainPage/MapInfo/GrpMiniMap");
            if (CutsceneCam == true)
            {
                if (CutsceneCamera == null)
                    if (GameObject.Find("UISceneRoot/CharacterSelectSceneNew(Clone)/Cam/") == null)
                    {
                        CutsceneCamera = GameObject.Find("StageRoot/LoginScene(Clone)/SceneObj/ModelCamera/LoginCamera(Clone)/");
                        offset = 0f;
                    }
                    else
                    {
                        CutsceneCamera = GameObject.Find("UISceneRoot/CharacterSelectSceneNew(Clone)/Cam/");
                        offset = 20f;
                    }
            }


            if (AvatarRoot)
            {
                try
                {
                    if (ActiveAvatar == null)
                        FindActiveAvatar();
                    if (!ActiveAvatar.activeInHierarchy)
                        FindActiveAvatar();
                }
                catch
                {
                }
            }
        }

        public void FindActiveAvatar()
        {
            foreach (var a in AvatarRoot.transform)
            {
                var active = a.Cast<Transform>();
                if (!active.gameObject.activeInHierarchy) continue;
                NewcamTarget = active;
                ActiveAvatar = active.gameObject;
            }

            String hi = ActiveAvatar.name;
            CharModel = GameObject.Find("/EntityRoot/AvatarRoot/" + hi + "/OffsetDummy/");
            Character = GameObject.Find("/EntityRoot/AvatarRoot/" + hi);
            String hi2 = hi.Substring(0, hi.Length - 7);
            String hi3 = "/EntityRoot/AvatarRoot/" + hi + "/OffsetDummy/" + hi2 + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 Neck/Bip001 Head/AO_Bip001 Head/";
            CharHead = GameObject.Find("/EntityRoot/AvatarRoot/" + hi + "/OffsetDummy/" + hi2 + "/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 Neck/Bip001 Head/AO_Bip001 Head/");
            
            //EntityRoot / AvatarRoot / Avatar_Lady_Sword_QinCostumeWic(Clone) / OffsetDummy / Avatar_Lady_Sword_QinCostumeWic / Bip001 / Bip001 Pelvis / Bip001 Spine / Bip001 Spine1 / Bip001 Spine2 / Bip001 Neck / Bip001 Head / AO_Bip001 Head 
        }

        private static bool Focused
        {
            get => Cursor.lockState == CursorLockMode.Locked;
            set
            {
                Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
                Cursor.visible = value == false;
            }
        }

        public static GameObject FindObject(GameObject parent, string name)
        {
            GameObject candidate = null;
            Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
            foreach (var t in trs)
            {
                if (t == null) continue;
                if (t.name == name)
                    candidate = t.gameObject;
            }

            Array.Clear(trs, 0, trs.Length);
            return candidate;
        }

        #endregion
    }
}