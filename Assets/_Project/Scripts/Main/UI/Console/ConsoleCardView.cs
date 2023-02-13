using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Main.UI.Console
{
    public class ConsoleCardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _messageText;

        public void Init(ConsoleView.LogLevel logLevel, string message)
        {
            _levelText.text = logLevel.ToString();
            _messageText.text = message;

            _levelText.color = logLevel switch
            {
                ConsoleView.LogLevel.Info => Color.cyan,
                ConsoleView.LogLevel.Warning => Color.yellow,
                ConsoleView.LogLevel.Error => Color.red,
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
            };
        }
    }
}