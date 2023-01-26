﻿using System;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using static QoL.Main;

namespace QoL
{
    public class LoadingScreen : MonoBehaviour
    {
        public LoadingScreen(IntPtr ptr) : base(ptr)
        {
        }

        public LoadingScreen() : base(ClassInjector.DerivedConstructorPointer<LoadingScreen>())
        {
            ClassInjector.DerivedConstructorBody(this);
        }

        public GameObject loadingPage;

        public void Update()
        {
            if (!DisableLoadingScreen) return;

            if (loadingPage == null)
                loadingPage = GameObject.Find("/RootCanvas(Clone)/Layer1/LoadingPage");
            if (loadingPage && loadingPage.activeInHierarchy)
                loadingPage.SetActive(false);
        }
    }
}