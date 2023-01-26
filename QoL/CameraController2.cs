using System;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using static FreeCam.Main2;

namespace FreeCam
{
    public class CameraController2 : MonoBehaviour
    {
        public CameraController2(IntPtr ptr) : base(ptr)
        {
        }

        public CameraController2() : base(ClassInjector.DerivedConstructorPointer<CameraController2>())
        {
            ClassInjector.DerivedConstructorBody(this);
        }

        private Transform _mainTransform;

        private void Awake()
        {
            _mainTransform = transform;
        }

        public void Update()
        {
            if (Newcam)
                Newcam.fieldOfView = NewcamFOV;
            if (Maincam)
                _mainTransform.rotation = Maincam.transform.rotation;
            if (NewcamTarget)
                _mainTransform.position = NewcamTarget.position + new Vector3(XOffset, YOffset, ZOffset) -
                                          _mainTransform.forward * DistanceFromTarget;
        }
    }
}