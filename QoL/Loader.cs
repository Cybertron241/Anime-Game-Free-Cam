using System;
using MelonLoader;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FreeCam
{
    public static class BuildInfo
    {
        public const string Name = "FreeCam";
        public const string Description = null;
        public const string Author = "Cybertron";
        public const string Company = null;
        public const string Version = "1.0.0";
        public const string DownloadLink = null;
    }

    public class Loader : MelonMod
    {
        public static GameObject IsRunning;
        public static Action<string> Msg;
        public static Action<string> Warning;
        public static Action<string> Error;

        public override void OnApplicationStart()
        {
            ClassInjector.RegisterTypeInIl2Cpp<Main2>();
            ClassInjector.RegisterTypeInIl2Cpp<CameraController2>();
            Msg = LoggerInstance.Msg;
            Warning = LoggerInstance.Warning;
            Error = LoggerInstance.Error;
        }

        public override void OnUpdate()
        {
            if ((!Input.GetKeyDown(KeyCode.I) || !Input.GetKey(KeyCode.LeftControl) || IsRunning != null)) return;

            IsRunning = new GameObject();
            IsRunning.AddComponent<Main2>();
            Object.DontDestroyOnLoad(IsRunning);
        }
    }
}