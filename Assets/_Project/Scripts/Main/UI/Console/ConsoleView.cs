using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Main.UI.Window;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Main.UI.Console
{
    public class ConsoleView : MonoBehaviour
    {
        [SerializeField] private WindowView _consoleWindowView;
        [SerializeField] private Toggle _infoToggle;
        [SerializeField] private Toggle _warningToggle;
        [SerializeField] private Toggle _errorToggle;
        [SerializeField] private Button _buttonClear;
        [SerializeField] private RectTransform _cardContainer;
        [SerializeField] private ConsoleCardView _consoleCardViewPrefab;

        private List<ConsoleCardView> _cards = new();
        private Dictionary<LogType, int> _messageCounts = new();

        private void Awake()
        {
            _infoToggle.onValueChanged.AddListener(OnInfoToggleSwitched);
            _warningToggle.onValueChanged.AddListener(OnWarningToggleSwitched);
            _errorToggle.onValueChanged.AddListener(OnErrorToggleSwitched);
            _buttonClear.onClick.AddListener(Clear);
        }

        private void OnDestroy()
        {
            _infoToggle.onValueChanged.RemoveListener(OnInfoToggleSwitched);
            _warningToggle.onValueChanged.RemoveListener(OnWarningToggleSwitched);
            _errorToggle.onValueChanged.RemoveListener(OnErrorToggleSwitched);
            _buttonClear.onClick.RemoveListener(Clear);
        }

        public void AddRecord(LogType logLevel, string conditionMessage, string stackTraceMessage)
        {
            var newCard = Instantiate(_consoleCardViewPrefab, _cardContainer);
            newCard.Setup(logLevel, conditionMessage, stackTraceMessage);
            _cards.Add(newCard);

            if (_messageCounts.TryGetValue(logLevel, out var count))
            {
                _messageCounts[logLevel] = ++count;
            }
        }

        public void Clear()
        {
            foreach (var cardView in _cards)
            {
                Destroy(cardView.gameObject);
            }
            
            _cards.Clear();
            _messageCounts.Clear();
        }

        public void SwitchView(bool state)
        {
            if (state)
            {
                _consoleWindowView.Show().Forget();
            }
            else
            {
                _consoleWindowView.Close().Forget();
            }
        }

        private void LogFiltering(LogType[] logTypes, bool state)
        {
            var cards = _cards.FindAll(x => logTypes.Contains(x.LogLevel));

            foreach (var cardView in cards)
            {
                cardView.gameObject.SetActive(state);
            }
        }

        private void OnInfoToggleSwitched(bool state)
        {
            var logTypes = new[] { LogType.Log };
            LogFiltering(logTypes, state);
        }

        private void OnErrorToggleSwitched(bool state)
        {
            var logTypes = new[] { LogType.Error, LogType.Assert, LogType.Exception };
            LogFiltering(logTypes, state);
        }

        private void OnWarningToggleSwitched(bool state)
        {
            var logTypes = new[] { LogType.Warning };
            LogFiltering(logTypes, state);
        }
    }
}