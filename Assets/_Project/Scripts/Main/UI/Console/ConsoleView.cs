using System.Collections.Generic;
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
        [SerializeField] private RectTransform _cardContainer;
        [SerializeField] private ConsoleCardView _consoleCardViewPrefab;

        private List<ConsoleCardView> _cards = new ();
        private Dictionary<LogLevel, int> _messageCounts = new ();

        public void AddRecord(LogLevel logLevel, string message)
        {
            var newCard = Instantiate(_consoleCardViewPrefab, _cardContainer);
            newCard.Init(logLevel, message);
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
                Destroy(cardView);
            }
            
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

        public enum LogLevel
        {
            Info,
            Warning,
            Error,
        }
    }
}