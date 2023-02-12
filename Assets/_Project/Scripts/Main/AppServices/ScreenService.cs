using _Project.Scripts.Extension;
using _Project.Scripts.Main.UI;
using Cysharp.Threading.Tasks;
using Tayx.Graphy;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace _Project.Scripts.Main.AppServices
{
    public class ScreenService : BaseService
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Volume _volume;
        [SerializeField] private GraphyManager _internalProfiler;
        [SerializeField] private bool _showProfilerOnStartup;
        [SerializeField] private AlertView _alertView;

        [Inject] private ControlService _controlService;

        public Camera MainCamera => _mainCamera;
        public VolumeProfile VolumeProfile => _volume.profile;

        private void Awake()
        {
            var controls = _controlService.Controls;
            _internalProfiler.enabled = _showProfilerOnStartup;
            controls.Player.InternalProfiler.BindAction(BindActions.Started, ctx => ToggleShowProfiler());
            _alertView.gameObject.SetActive(false);
        }

        public void ShowAlert(string title, string message)
        {
            _alertView.Show(title, message).Forget();
        }
        
        private void ToggleShowProfiler()
        {
            _internalProfiler.enabled = !_internalProfiler.enabled;
        }
    }
}