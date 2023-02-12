using System;
using _Project.Scripts.Main.UI.Window;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _bodyText;

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
            _titleText.text = title;
            _bodyText.text = bodyText;
            gameObject.SetActive(true);
            await Show();
        }
        
        public async UniTask Close()
        {
            await Close();
            gameObject.SetActive(false);
        }

        private void Dismiss()
        {
            OnDismiss?.Invoke();
        }

        public void Disable()
        {
            _canvasGroup.interactable = false;
        }
        
        public void Enable()
        {
            _canvasGroup.interactable = true;
        }
    }
}