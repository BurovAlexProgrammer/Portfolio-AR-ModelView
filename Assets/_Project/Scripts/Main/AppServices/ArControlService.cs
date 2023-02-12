using System;
using UnityEngine;
using Vuforia;

namespace _Project.Scripts.Main.AppServices
{
    public class ArControlService : IDisposable
    {
        private VuforiaApplication _vuforiaApplication;
        // private VuforiaBehaviour _vuforiaBehaviour;
        
        public event Action<VuforiaInitError> OnVuforiaInitialized;

        public ArControlService()
        {
            _vuforiaApplication = VuforiaApplication.Instance;
            // _vuforiaBehaviour = VuforiaBehaviour.Instance; 
            _vuforiaApplication.OnVuforiaInitialized += VuforiaInitialized;
        }
        
        public void Dispose()
        {
            _vuforiaApplication.OnVuforiaInitialized -= VuforiaInitialized;
            Debug.LogError("VuforiaControl service disposed. ");
        }

        private void VuforiaInitialized(VuforiaInitError error)
        {
            Debug.LogError("Vuforia initialized");
            VerifyPoseSensor();
        }

        private void VerifyPoseSensor()
        {
            if (VuforiaBehaviour.Instance.World.AnchorsSupported)
            {
                if (!VuforiaBehaviour.Instance.DevicePoseBehaviour.enabled)
                {
                    Debug.LogError("The Ground Plane feature requires the Device Tracking to be started. " +
                                   "Please enable it in the Vuforia Configuration or start it at runtime through the scripting API.");
                    return;
                }

                Debug.Log("DevicePoseBehaviour is Active");
            }
            else
            {
                Services.ScreenService.ShowAlert("Ttt", "Goood !!");
                Debug.LogError("Pose Sensor not found.");
                //MessageBox.DisplayMessageBox(UNSUPPORTED_DEVICE_TITLE, UNSUPPORTED_DEVICE_BODY, false, null);
            }
        }
    }
}