using System;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.UI.Window;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Main.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class AlertView : WindowView
    {
        [SerializeField] private Button _buttonDismiss;
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _bodyText;

        private CursorLockMode _lastCursorMode;
        
        public event Action OnDismiss;

        private const float FadeDuration = 0.3f;

        private void Awake()
        {
            _buttonDismiss.onClick.AddListener(Dismiss);
        }

        private void OnDestroy()
        {
            _buttonDismiss.onClick.RemoveListener(Dismiss);
        }

        public async UniTask Show(string title, string bodyText)
        {
            _lastCursorMode = Services.ControlService.CursorLockState;
            _titleText.text = title;
            _bodyText.text = bodyText;
            gameObject.SetActive(true);
            await Show();
            Services.ControlService.UnlockCursor();
            Services.ControlService.DisableControls();
        }


        private void Dismiss()
        {
            UniTask.Void( async () =>
            {
                await Close();
                
                if (_lastCursorMode is CursorLockMode.None or CursorLockMode.Confined)
                {
                    Services.ControlService.UnlockCursor();
                }
                
                Services.ControlService.EnableControls();
                OnDismiss?.Invoke();
            });
        }
    }
}