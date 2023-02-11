using _Project.Scripts.Main.AppServices;
using UnityEngine;

namespace _Project.Scripts.Main
{
    public class CameraDisabler : MonoBehaviour
    {
        private ScreenService _screenService;
        
        private void Awake()
        {
            _screenService = Services.ScreenService;
        }

        private void Start()
        {
            
            _screenService.MainCamera.gameObject.SetActive(false);
            Debug.LogWarning("Camera was disabled by CameraDisabler (Click to select)", this);
        }

        private void OnDestroy()
        {
            if (_screenService == null || _screenService.MainCamera == null) return;
            ReturnCameraToService();
        }

        public void ReturnCameraToService()
        {
            _screenService.MainCamera.gameObject.SetActive(true);
            Debug.Log("Camera was enabled by CameraDisabler (Click to select)", this);
        }
    }
}