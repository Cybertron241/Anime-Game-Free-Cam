using System;
using MelonLoader;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace QoL
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
            ClassInjector.RegisterTypeInIl2Cpp<Cooldown>();
            ClassInjector.RegisterTypeInIl2Cpp<Main>();
            ClassInjector.RegisterTypeInIl2Cpp<CameraController>();
            ClassInjector.RegisterTypeInIl2Cpp<LoadingScreen>();
            Msg = LoggerInstance.Msg;
            Warning = LoggerInstance.Warning;
            Error = LoggerInstance.Error;
        }

        public override void OnUpdate()
        {
            if (!Input.GetKeyDown(KeyCode.I) || IsRunning != null) return;

            IsRunning = new GameObject();
            IsRunning.AddComponent<Main>();
            Object.DontDestroyOnLoad(IsRunning);
        }
    }
}