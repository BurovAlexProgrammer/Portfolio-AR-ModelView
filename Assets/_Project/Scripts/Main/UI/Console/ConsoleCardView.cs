using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Main.UI.Console
{
    public class ConsoleCardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _buttonCopyStack;

        private LogType _logLevel;
        private string _stackTraceMessage;

        public LogType LogLevel => _logLevel;

        private void Awake()
        {
            _buttonCopyStack.onClick.AddListener(CopyStackTraceToClipboard);
        }

        private void OnDestroy()
        {
            _buttonCopyStack.onClick.RemoveListener(CopyStackTraceToClipboard);
        }

        private void CopyStackTraceToClipboard()
        {
            GUIUtility.systemCopyBuffer = _stackTraceMessage;
        }

        public void Setup(LogType logLevel, string condition, string stackTraceMessage)
        {
            _logLevel = logLevel;
            _levelText.text = logLevel.ToString();
            _messageText.text = condition;
            _stackTraceMessage = stackTraceMessage;

            _levelText.color = logLevel switch
            {
                LogType.Log or LogType.Assert => Color.white,
                LogType.Warning => Color.yellow,
                LogType.Error or LogType.Exception => Color.red,
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
            };
        }
    }
}