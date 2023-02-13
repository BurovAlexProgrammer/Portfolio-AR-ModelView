using _Project.Scripts.Extension;
using _Project.Scripts.Main.UI;
using _Project.Scripts.Main.UI.Console;
using Cysharp.Threading.Tasks;
using Tayx.Graphy;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
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
        [SerializeField] private ConsoleView _consoleView;
        [SerializeField] private Toggle _profilerSwitcher;
        [SerializeField] private Toggle _consoleSwitcher;

        [Inject] private ControlService _controlService;
        
        private Controls Controls => _controlService.Controls;
        public Camera MainCamera => _mainCamera;
        public VolumeProfile VolumeProfile => _volume.profile;

        private void Awake()
        {
            _internalProfiler.enabled = _showProfilerOnStartup;
            Controls.Player.InternalProfiler.BindAction(BindActions.Started, SwitchProfiler);
            _alertView.gameObject.SetActive(false);
            _profilerSwitcher.onValueChanged.AddListener(SwitchProfiler);
            _consoleSwitcher.onValueChanged.AddListener(SwitchConsole);
            SwitchProfiler(_profilerSwitcher.isOn);
            SwitchConsole(_consoleSwitcher.isOn);
            Application.logMessageReceived += OnLogMessageReceived;
        }

        private void OnLogMessageReceived(string condition, string stacktrace, LogType logLevel)
        {
            _consoleView.AddRecord(logLevel, condition, stacktrace);
        }

        private void OnDestroy()
        {
            Controls.Player.InternalProfiler.UnbindAction(BindActions.Started,  SwitchProfiler);
            _profilerSwitcher.onValueChanged.RemoveListener(SwitchProfiler);
            _consoleSwitcher.onValueChanged.RemoveListener(SwitchConsole);
            Application.logMessageReceived -= OnLogMessageReceived;
        }

        public void ShowAlert(string title, string message)
        {
            _alertView.Show(title, message).Forget();
        }

        private void SwitchProfiler(InputAction.CallbackContext ctx)
        {
            _internalProfiler.enabled = !_internalProfiler.enabled;
            _profilerSwitcher.isOn = _internalProfiler.enabled;
        }
        
        private void SwitchProfiler(bool value)
        {
            _internalProfiler.enabled = value;
        }
        
        private void SwitchConsole(bool state)
        {
            _consoleView.SwitchView(state);
        }
    }
}